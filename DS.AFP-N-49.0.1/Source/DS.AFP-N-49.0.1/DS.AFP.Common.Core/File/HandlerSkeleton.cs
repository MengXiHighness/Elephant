
using System;
using System.IO;
using System.Collections;



namespace DS.AFP.Common.Core
{
	
	public abstract class HandlerSkeleton : IHandler,  IOptionHandler
	{
		#region 保护构造函数

		
		protected HandlerSkeleton()
		{
			
		}

		#endregion Protected Instance Constructors

		#region 资源回收

		
        ~HandlerSkeleton() 
		{
			if (!m_closed) 
			{
				
				Close();
			}
		}

		#endregion Finalizer

		#region 配置处理

		
		virtual public void InitOptions() 
		{
		}

		#endregion 

		#region IHandler

		
		public string Name 
		{
			get { return m_name; }
			set { m_name = value; }
		}

		
		public void Close()
		{
			
			lock(this)
			{
				if (!m_closed)
				{
					OnClose();
					m_closed = true;
				}
			}
		}


        public void ExecuteHandler(PersistentData data) 
		{
			
			lock(this)
			{
				if (m_closed)
				{
					return;
				}

				// prevent re-entry
				if (m_recursiveGuard)
				{
					return;
				}

				try
				{
					m_recursiveGuard = true;

					if (PreAppendCheck())
					{
						this.Handler(data);
					}
				}
				catch(Exception ex)
				{
				}
				finally
				{
					m_recursiveGuard = false;
				}
			}
		}

		#endregion 

		#region 

		

		
		virtual protected void OnClose() 
		{
			// Do nothing by default
		}

		
        abstract protected void Handler(PersistentData data);

		
        virtual protected void Handler(PersistentData[] datas)
		{
            foreach (PersistentData data in datas)
			{
				Handler(data);
			}
		}

		
		virtual protected bool PreAppendCheck()
		{
			return true;
		}

		
        protected string RenderDataEvent(PersistentData data)
		{
			// Create the render writer on first use
			if (m_renderWriter == null)
			{
				m_renderWriter = new ReusableStringWriter(System.Globalization.CultureInfo.InvariantCulture);
			}

            lock (m_renderWriter)
            {
               
                m_renderWriter.Reset(c_renderBufferMaxCapacity, c_renderBufferSize);

                RenderDataEvent(m_renderWriter, data);
                return m_renderWriter.ToString();
            }
		}


        protected void RenderDataEvent(TextWriter writer, PersistentData data)
		{
			string exceptionStr = data.Data;
			if (exceptionStr != null && exceptionStr.Length > 0) 
			{
				
				//m_layout.Format(writer, loggingEvent);
				writer.WriteLine(exceptionStr);
			}
		}

	
		#endregion

		#region Private Instance Fields

		

		/// <summary>
		/// Handler 名称
		/// </summary>
		/// <remarks>
		/// See <see cref="Name"/> 
		/// </remarks>
		private string m_name;
        
		

		/// <summary>
		/// 标记Handler是否关闭
		/// </summary>
		/// <remarks>
		/// See <see cref="Close"/> 
		/// </remarks>
		private bool m_closed = false;

		/// <summary>
		/// 防止重复处理
		/// </summary>
		private bool m_recursiveGuard = false;

		/// <summary>
		/// StringWriter 
		/// </summary>
		private ReusableStringWriter m_renderWriter = null;

		#endregion Private Instance Fields

		#region 常量

		/// <summary>
		/// 初始化buff大小
		/// </summary>
		private const int c_renderBufferSize = 256;

		/// <summary>
		/// 循环之前的缓冲大小
		/// </summary>
		private const int c_renderBufferMaxCapacity = 1024;

		#endregion

	    #region Private Static Fields

	  
        private readonly static Type declaringType = typeof(HandlerSkeleton);

	    #endregion
	}
}
