using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    public class TextReaderAdapter:TextReader
    {
        private TextReader m_reader;

        #region 构造函数
        protected TextReaderAdapter(TextReader reader)            
		{
            m_reader = reader;
        }

        #endregion

        #region 保护的属性
        protected TextReader Reader
        {
            get { return m_reader; }
            set { m_reader = value; }
        }
        #endregion 

        #region 重载
        public override void Close()
        {
            m_reader.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((IDisposable)m_reader).Dispose();
            }
        }

        public override string ReadLine()
        {
            return m_reader.ReadLine();
        }

        public override string ReadToEnd()
        {
            return m_reader.ReadToEnd();
        }

        public override int Read()
        {
            return m_reader.Read();
        }

        public override int Peek()
        {
            return m_reader.Peek();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            return m_reader.Read(buffer, index, count);
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return m_reader.ReadBlock(buffer, index, count);
        }

        #endregion

    }
}
