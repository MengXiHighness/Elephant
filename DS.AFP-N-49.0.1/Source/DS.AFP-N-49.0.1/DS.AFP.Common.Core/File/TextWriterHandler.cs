
using System;
using System.IO;


namespace DS.AFP.Common.Core
{
	
	public class TextWriterHandler : HandlerSkeleton
	{
		#region 构造

		
		public TextWriterHandler() 
		{
		}

		
		
		public TextWriterHandler(Stream os) : this(new StreamWriter(os))
		{
		}

		
		
        public TextWriterHandler(TextWriter writer) 
		{
			
			Writer = writer;
		}

		#endregion

		#region 公共属性

		
		public bool ImmediateFlush 
		{
			get { return m_immediateFlush; }
			set { m_immediateFlush = value; }
		}

	
		virtual public TextWriter Writer 
		{
			get { return m_qtw; }
			set 
			{
				lock(this) 
				{
					Reset();
					if (value != null)
					{
						m_qtw = new QuietTextWriter(value);
						
					}
				}
			}
		}

		#endregion 

		#region 重载

		
		override protected bool PreAppendCheck() 
		{
			if (!base.PreAppendCheck()) 
			{
				return false;
			}

			if (m_qtw == null) 
			{
				
				PrepareWriter();

				if (m_qtw == null) 
				{
					
					return false;
				}
			}
			if (m_qtw.Closed) 
			{
				
				return false;
			}

			return true;
		}

		
        override protected void Handler(PersistentData data) 
		{
            RenderDataEvent(m_qtw, data);

			if (m_immediateFlush) 
			{
				m_qtw.Flush();
			} 
		}

		
        override protected void Handler(PersistentData[] datas) 
		{
            foreach (PersistentData loggingEvent in datas)
			{
                RenderDataEvent(m_qtw, loggingEvent);
			}

			if (m_immediateFlush) 
			{
				m_qtw.Flush();
			} 
		}

		
		override protected void OnClose() 
		{
			lock(this)
			{
				Reset();
			}
		}

		
	

		#endregion 

		#region 保护方法

		
		virtual protected void CloseWriter() 
		{
			if (m_qtw != null) 
			{
				try 
				{
					m_qtw.Close();
				} 
				catch(Exception e) 
				{
					
					
				}
			}
		}

		
		virtual protected void Reset() 
		{
            CloseWriter();
			m_qtw = null;
		}

		

		virtual protected void PrepareWriter()
		{
		}

		
		protected QuietTextWriter QuietWriter
		{
			get { return m_qtw; }
			set { m_qtw = value; }
        }

        #endregion Protected Instance Methods

        #region 私有属性

      
		private QuietTextWriter m_qtw;

		
		private bool m_immediateFlush = true;

		#endregion Private Instance Fields

	    #region Private Static Fields

	   
	    private readonly static Type declaringType = typeof(TextWriterHandler);

	    #endregion Private Static Fields
	}
}
