
using System;
using System.IO;
using System.Text;
using System.Threading;


namespace DS.AFP.Common.Core
{

	
    public class FileHandler : TextWriterHandler 
	{
		#region LockingStream Inner Class
		
		private sealed class LockingStream : Stream, IDisposable
		{
			
			private Stream m_realStream=null;
			private LockingModelBase m_lockingModel=null;
			private int m_readTotal=-1;
			private int m_lockLevel=0;

			public LockingStream(LockingModelBase locking) : base()
			{
				if (locking==null)
				{
					throw new ArgumentException("Locking model may not be null","locking");
				}
				m_lockingModel=locking;
			}

			#region 重载流

			
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				AssertLocked();
				IAsyncResult ret=m_realStream.BeginRead(buffer,offset,count,callback,state);
				m_readTotal=EndRead(ret);
				return ret;
			}

			
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				AssertLocked();
				IAsyncResult ret=m_realStream.BeginWrite(buffer,offset,count,callback,state);
				EndWrite(ret);
				return ret;
			}

			public override void Close() 
			{
				m_lockingModel.CloseFile();
			}

			public override int EndRead(IAsyncResult asyncResult) 
			{
				AssertLocked();
				return m_readTotal;
			}
			public override void EndWrite(IAsyncResult asyncResult) 
			{
				
			}
			public override void Flush() 
			{
				AssertLocked();
				m_realStream.Flush();
			}
			public override int Read(byte[] buffer, int offset, int count) 
			{
				return m_realStream.Read(buffer,offset,count);
			}
			public override int ReadByte() 
			{
				return m_realStream.ReadByte();
			}
			public override long Seek(long offset, SeekOrigin origin) 
			{
				AssertLocked();
				return m_realStream.Seek(offset,origin);
			}
			public override void SetLength(long value) 
			{
				AssertLocked();
				m_realStream.SetLength(value);
			}
			void IDisposable.Dispose() 
			{
				Close();
			}
			public override void Write(byte[] buffer, int offset, int count) 
			{
				AssertLocked();
				m_realStream.Write(buffer,offset,count);
			}
			public override void WriteByte(byte value) 
			{
				AssertLocked();
				m_realStream.WriteByte(value);
			}

			// Properties
			public override bool CanRead 
			{ 
				get { return false; } 
			}
			public override bool CanSeek 
			{ 
				get 
				{
					AssertLocked();
					return m_realStream.CanSeek;
				} 
			}
			public override bool CanWrite 
			{ 
				get 
				{
					AssertLocked();
					return m_realStream.CanWrite;
				} 
			}
			public override long Length 
			{ 
				get 
				{
					AssertLocked();
					return m_realStream.Length;
				} 
			}
			public override long Position 
			{ 
				get 
				{
					AssertLocked();
					return m_realStream.Position;
				} 
				set 
				{
					AssertLocked();
					m_realStream.Position=value;
				} 
			}

			#endregion Override Implementation of Stream

			#region 锁

			private void AssertLocked()
			{
				if (m_realStream == null)
				{
					//throw new LockStateException("The file is not currently locked");
				}
			}

			public bool AcquireLock()
			{
				bool ret=false;
				lock(this)
				{
					if (m_lockLevel==0)
					{
						
						m_realStream=m_lockingModel.AcquireLock();
					}
					if (m_realStream!=null)
					{
						m_lockLevel++;
						ret=true;
					}
				}
				return ret;
			}

			public void ReleaseLock()
			{
				lock(this)
				{
					m_lockLevel--;
					if (m_lockLevel==0)
					{
						
						m_lockingModel.ReleaseLock();
						m_realStream=null;
					}
				}
			}

			#endregion Locking Methods
		}

		#endregion 

		#region 锁对象

		
		public abstract class LockingModelBase
		{
            private FileHandler m_appender = null;

			
			public abstract void OpenFile(string filename, bool append,Encoding encoding);

			
			public abstract void CloseFile();

			
			public abstract Stream AcquireLock();

			
			public abstract void ReleaseLock();


            public FileHandler CurrentHandler
			{
				get { return m_appender; }
				set { m_appender = value; }
			}

            
            protected Stream CreateStream(string filename, bool append, FileShare fileShare)
            {
                using (CurrentHandler.SecurityContext.Impersonate(this))
                {
                    // Ensure that the directory structure exists
                    string directoryFullName = Path.GetDirectoryName(filename);

                    // Only create the directory if it does not exist
                    // doing this check here resolves some permissions failures
                    if (!Directory.Exists(directoryFullName))
                    {
                        Directory.CreateDirectory(directoryFullName);
                    }

                    FileMode fileOpenMode = append ? FileMode.Append : FileMode.Create;
                    return new FileStream(filename, fileOpenMode, FileAccess.Write, fileShare);
                }
            }

           
            protected void CloseStream(Stream stream)
            {
                using (CurrentHandler.SecurityContext.Impersonate(this))
                {
                    stream.Close();
                }
           }
		}

		
		public class ExclusiveLock : LockingModelBase
		{
			private Stream m_stream = null;

			
			public override void OpenFile(string filename, bool append,Encoding encoding)
			{
				try
				{
                    m_stream = CreateStream(filename, append, FileShare.Read);
				}
				catch (Exception e1)
				{
					//CurrentAppender.ErrorHandler.Error("Unable to acquire lock on file "+filename+". "+e1.Message);
				}
			}

		
			public override void CloseFile()
			{
                CloseStream(m_stream);
                m_stream = null;
			}

			
			public override Stream AcquireLock()
			{
				return m_stream;
			}

			
			public override void ReleaseLock()
			{
				//NOP
			}
		}

		
		public class MinimalLock : LockingModelBase
		{
			private string m_filename;
			private bool m_append;
			private Stream m_stream=null;

			
			public override void OpenFile(string filename, bool append, Encoding encoding)
			{
				m_filename=filename;
				m_append=append;
			}

			
			public override void CloseFile()
			{
				// NOP
			}

			
			public override Stream AcquireLock()
			{
				if (m_stream==null)
				{
					try
					{
                        m_stream = CreateStream(m_filename, m_append, FileShare.Read);
                        m_append = true;
					}
					catch (Exception e1)
					{
						
					}
				}
				return m_stream;
			}

			
			public override void ReleaseLock()
			{
                CloseStream(m_stream);
                m_stream = null;
			}
		}


		#endregion 

		#region 构造函数

	
		public FileHandler()
		{
		}

		
		[Obsolete("Instead use the default constructor and set the Layout, File & AppendToFile properties")]
		public FileHandler(string filename, bool append) 
		{
			
			File = filename;
			AppendToFile = append;
			InitOptions();
		}

		
		[Obsolete("Instead use the default constructor and set the Layout & File properties")]
        public FileHandler(string filename)
            : this(filename, true)
		{
		}

		#endregion 

		#region 公共属性

		
		virtual public string File
		{
			get { return m_fileName; }
			set { m_fileName = value; }
		}

		
		public bool AppendToFile
		{
			get { return m_appendToFile; }
			set { m_appendToFile = value; }
		}

		
		public Encoding Encoding
		{
			get { return m_encoding; }
			set { m_encoding = value; }
		}

		
		public SecurityContext SecurityContext 
		{
			get { return m_securityContext; }
			set { m_securityContext = value; }
		}


		public FileHandler.LockingModelBase LockingModel
		{
			get { return m_lockingModel; }
			set { m_lockingModel = value; }
		}

		#endregion 

		#region 重载

		
		override public void InitOptions() 
		{	
			base.InitOptions();

			if (m_securityContext == null)
			{
				m_securityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
			}

			if (m_lockingModel == null)
			{
				m_lockingModel = new FileHandler.ExclusiveLock();
			}

			m_lockingModel.CurrentHandler=this;

			using(SecurityContext.Impersonate(this))
			{
				m_fileName = ConvertToFullPath(m_fileName.Trim());
			}

			if (m_fileName != null) 
			{
				SafeOpenFile(m_fileName, m_appendToFile);
			} 
			else 
			{
				
			}
		}

		#endregion 

		#region 重载 TextWriterAppender

		
		override protected void Reset() 
		{
			base.Reset();
			m_fileName = null;
		}

 		
 		override protected void PrepareWriter()
 		{
			SafeOpenFile(m_fileName, m_appendToFile);
 		}


        override protected void Handler(PersistentData data) 
		{
			if (m_stream.AcquireLock())
			{
				try
				{
                    base.Handler(data);
				}
				finally
				{
					m_stream.ReleaseLock();
				}
			}
		}

		
        override protected void Handler(PersistentData[] datas) 
		{
			if (m_stream.AcquireLock())
			{
				try
				{
					base.Handler(datas);
				}
				finally
				{
					m_stream.ReleaseLock();
				}
			}
		}

	
		protected override void CloseWriter() 
		{
			if (m_stream!=null)
			{
				m_stream.AcquireLock();
				try
				{
					base.CloseWriter();
				}
				finally
				{
					m_stream.ReleaseLock();
				}
			}
		}

		#endregion

		#region 公共方法

		
		protected void CloseFile() 
		{
			CloseWriter();
		}

		#endregion 

		#region 保护方法

		
		virtual protected void SafeOpenFile(string fileName, bool append)
		{
			try 
			{
				OpenFile(fileName, append);
			}
			catch(Exception e) 
			{
				
			}
		}

		
		virtual protected void OpenFile(string fileName, bool append)
		{
          

			lock(this)
			{
				Reset();

				
				m_fileName = fileName;
				m_appendToFile = append;

				LockingModel.CurrentHandler=this;
				LockingModel.OpenFile(fileName,append,m_encoding);
				m_stream=new LockingStream(LockingModel);

				if (m_stream != null)
				{
					m_stream.AcquireLock();
					try
					{
						SetQWForFiles(new StreamWriter(m_stream, m_encoding));
					}
					finally
					{
						m_stream.ReleaseLock();
					}
				}
			}
		}

		
		virtual protected void SetQWForFiles(Stream fileStream) 
		{
			SetQWForFiles(new StreamWriter(fileStream, m_encoding));
		}
		
		virtual protected void SetQWForFiles(TextWriter writer) 
		{
			QuietWriter = new QuietTextWriter(writer);
		}

		#endregion 

		#region 保护静态方法

		
		protected static string ConvertToFullPath(string path)
		{
            return path;//SystemInfo.ConvertToFullPath(path);
		}

		#endregion Protected Static Methods

		#region 私有属性

		
		private bool m_appendToFile = true;

		
		private string m_fileName = null;

		
		private Encoding m_encoding = Encoding.Default;

		
		private SecurityContext m_securityContext;

		
		private FileHandler.LockingStream m_stream = null;

		/// <summary>
		/// The locking model to use
		/// </summary>
        private FileHandler.LockingModelBase m_lockingModel = new FileHandler.ExclusiveLock();

		#endregion 

	    #region 私有静态属性

	  
        private readonly static Type declaringType = typeof(FileHandler);

	    #endregion
	}
}
