
using System;
using System.IO;



namespace DS.AFP.Common.Core
{
	
	public class QuietTextWriter : TextWriterAdapter
	{
		#region 构造函数

		
		public QuietTextWriter(TextWriter writer) : base(writer)
		{
			
			
		}

		#endregion 

        #region 公共属性


        public bool Closed
		{
			get { return m_closed; }
		}

		#endregion 公共属性

		#region 重载

		
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

		#region 私有属性

		
		private bool m_closed = false;

		#endregion 
	}
}
