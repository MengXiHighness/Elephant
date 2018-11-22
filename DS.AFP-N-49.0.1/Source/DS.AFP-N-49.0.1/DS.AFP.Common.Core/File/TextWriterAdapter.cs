
using System;
using System.Text;
using System.IO;
using System.Globalization;

namespace DS.AFP.Common.Core
{

	public abstract class TextWriterAdapter : TextWriter
	{
		#region Private Member Variables

		private TextWriter m_writer;

		#endregion

		#region Constructors

		
		protected TextWriterAdapter(TextWriter writer) :  base(CultureInfo.InvariantCulture)
		{
			m_writer = writer;
		}

		#endregion

		#region Protected Instance Properties

		
		protected TextWriter Writer 
		{
			get { return m_writer; }
			set { m_writer = value; }
		}

		#endregion Protected Instance Properties

		#region Public Properties
    
		
		override public Encoding Encoding 
		{
			get { return m_writer.Encoding; }
		}

		
		override public IFormatProvider FormatProvider 
		{
			get { return m_writer.FormatProvider; }
		}

		
		override public String NewLine 
		{
			get { return m_writer.NewLine; }
			set { m_writer.NewLine = value; }
		}

		#endregion

		#region Public Methods

		
		override public void Close() 
		{
			m_writer.Close();
		}

		
		override protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDisposable)m_writer).Dispose();
			}
		}

		
		override public void Flush() 
		{
			m_writer.Flush();
		}

		
		override public void Write(char value) 
		{
			m_writer.Write(value);
		}
    
		
		override public void Write(char[] buffer, int index, int count) 
		{
			m_writer.Write(buffer, index, count);
		}
    
		
		override public void Write(String value) 
		{
			m_writer.Write(value);
		}

		#endregion
	}
}
