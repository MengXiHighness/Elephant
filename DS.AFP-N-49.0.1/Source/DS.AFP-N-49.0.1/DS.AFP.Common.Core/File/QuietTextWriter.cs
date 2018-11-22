
using System;
using System.IO;



namespace DS.AFP.Common.Core
{
	
	public class QuietTextWriter : TextWriterAdapter
	{
		#region ���캯��

		
		public QuietTextWriter(TextWriter writer) : base(writer)
		{
			
			
		}

		#endregion 

        #region ��������


        public bool Closed
		{
			get { return m_closed; }
		}

		#endregion ��������

		#region ����

		
		public override void Write(char value) 
		{
			try 
			{
				base.Write(value);
			} 
			catch(Exception e) 
			{
				
			}
		}
    
	
		public override void Write(char[] buffer, int index, int count) 
		{
			try 
			{
				base.Write(buffer, index, count);
			} 
			catch(Exception e) 
			{
				
			}
		}
    
	
		override public void Write(string value) 
		{
			try 
			{
				base.Write(value);
			} 
			catch(Exception e) 
			{
				
			}
		}

		
		override public void Close()
		{
			m_closed = true;
			base.Close();
		}

		#endregion

		#region ˽������

		
		private bool m_closed = false;

		#endregion 
	}
}
