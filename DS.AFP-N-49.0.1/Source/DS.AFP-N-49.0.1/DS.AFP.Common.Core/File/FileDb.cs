
using System;
using System.Collections;



namespace DS.AFP.Common.Core
{
	
	public  class FileDB : IFileDB
	{
		#region ���캯��

		
        
		#endregion 

		#region ��������

		
		virtual public bool Additivity
		{
			get { return m_additive; }
			set { m_additive = value; }
		}

     
		#endregion 

		#region ʵ�� IHandlerAttachable

		
        public bool IsExistHandler(string handlerKey)
        {
            return sessionHandlerCollection.ContainsKey(handlerKey);
        }

		virtual public void AddHandler(IHandler newHandler) 
		{
            if (newHandler == null)
			{
                throw new ArgumentNullException("newHandler");
			}

            try
            {
                if (newHandler.Name != null)
                {
                    if (!sessionHandlerCollection.ContainsKey(newHandler.Name))
                    {
                        sessionHandlerCollection.TryAdd(newHandler.Name, newHandler);
                        newHandler.InitOptions();
                    }
                    else
                    {
                        //�Ѿ�����
                    }
                }
                else
                {
                    throw new NullReferenceException("Name of IHandler is null");
                }
            }
            finally { }
			
		}


        virtual public SessionHandlerCollection SessionHandlerCollection 
		{
			get
			{
                return sessionHandlerCollection;
			}
		}

		
		virtual public IHandler GetHandler(string handlerKey) 
		{
            IHandler handler = null;
            if (handlerKey == null)
			{
				return null;
			}

            sessionHandlerCollection.TryGetValue(handlerKey,out handler);
            return handler;
		}

		
		virtual public void RemoveAllHandler() 
		{
            sessionHandlerCollection.Clear();
		}
	
		virtual public IHandler RemoveHandler(string handlerKey) 
		{
            IHandler handler = null;
            sessionHandlerCollection.TryRemove(handlerKey, out handler);
            handler.Close();
            return handler;
		}
  
		#endregion

		#region ʵ�� IFileDb

		
		virtual public string Name
		{
			get { return m_name; }
		}


        public void Write(PersistentData data)
		{
			try
			{
				ForcedWrite(data);
			}
			catch (Exception ex)
			{
				
			}

		}

  		#endregion 

		
        virtual protected void CallHandler(PersistentData data) 
		{
			if (data == null)
			{
                throw new ArgumentNullException("PersistentData");
			}

			int writes = 0;

			this.m_appenderLock.AcquireReaderLock();
			try
			{
                IHandler handler = null;
                sessionHandlerCollection.TryGetValue(data.SessionID, out handler);
                if (handler != null)
                    handler.ExecuteHandler(data);
                else
                {
                    //handlerû��ע����߱��Ƴ�
                }
			}
			finally
			{
				this.m_appenderLock.ReleaseReaderLock();
			}
			
		}
	
		virtual public void CloseHandler(string handlerKey) 
		{
           
			m_appenderLock.AcquireWriterLock();
			try
			{
                IHandler handler = null;
                sessionHandlerCollection.TryRemove(handlerKey, out handler);
                if(handler!=null)
                    handler.Close();
			}
			finally
			{
				m_appenderLock.ReleaseWriterLock();
			}
		}

        virtual protected void ForcedWrite(PersistentData data) 
		{
            CallHandler(data);
		}

        public bool ContainsHandler(string handlerKey)
        {
            return sessionHandlerCollection.ContainsKey(handlerKey);
        }

		#region ˽�о�̬����

      
        private readonly static Type declaringType = typeof(FileDB);

        private static SessionHandlerCollection sessionHandlerCollection = new SessionHandlerCollection();

		#endregion 

		#region ˽�к���

		
		private readonly string m_name;  

		
		private bool m_additive = true;

        private readonly ReaderWriterLock m_appenderLock = new ReaderWriterLock();

		#endregion
	}
}
