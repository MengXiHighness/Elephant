using DS.AFP.Common.Core.Utility;
using System;
using System.Xml;
using DS.AFP.Common.Core;

namespace DS.AFP.Data
{
	/// <summary>
	/// DataForm ��ժҪ˵����
	/// </summary>
    [Serializable]
	public class DataForm:DataNode
	{
       

		#region ���캯��

		/// <summary>
		/// �յ�DataForm���캯��
		/// </summary>
		public DataForm():base()
		{
			this.NodeType="Form";
			this.Init();
		}


        private void Clear()
        {
            this.XmlEle.RemoveAll();
        }

        /// <summary>
        /// ����XML�ַ�������DataForm
        /// </summary>
        /// <param name="xmlsrc"></param>
        public DataForm(string xmlsrc)
            : base(xmlsrc)
        {
            this.NodeType = "Form";
        }

		/// <summary>
		/// ����DataForm�����µ�DataForm
		/// </summary>
		/// <param name="xmlsrc"></param>
		public DataForm(DataForm dataForm):base(dataForm.ToString())
		{
            this.NodeType = "Form";
		}
		/// <summary>
		/// ����XmlElement����DataForm
		/// </summary>
		/// <param name="ele"></param>
		public DataForm(XmlElement ele):base(ele)
		{
            this.NodeType = "Form";
		}

		/// <summary>
		/// ����XmlDocument��XmlElement����DataForm
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="ele"></param>
		internal DataForm(XmlDocument doc,XmlElement ele):base(doc,ele)
		{
            this.NodeType = "Form";
		}

		#endregion


		#region ���з���
		/// <summary>
		/// ����ӽڵ�
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public DataItem AddItem(string name,string value)
		{
			XmlElement node=this.XmlDoc.CreateElement("item");
			this.XmlEle.AppendChild(node);
			node.SetAttribute("name",name);
            node.SetAttribute("value", value);
			return new DataItem(this.XmlDoc,node);
		}

		/// <summary>
		/// ����name��ȡ�ӽڵ��ֵ
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetValue(string name)
		{
            XmlNode node = this.XmlEle.SelectSingleNode("/form/item[@name='{0}']".FormatString(name));
			if(node==null) return null;
            return XmlHelper.FilterNull(node.GetAttribute("value").Value);
		}

		/// <summary>
		/// ����name�����ӽڵ��ֵ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetValue(string name,string value)
		{
			XmlNode node=this.XmlEle.SelectSingleNode("/form/item[@name='{0}']".FormatString(name));
			if(node==null)
			{
                this.AddItem(name, value);
                return;
			}
			node.GetAttribute("value").Value=value;
		}

		/// <summary>
		/// ����name��ȡ�ӽڵ��XMLֵ
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetInnerXml(string name)
		{
            XmlNode node = this.XmlEle.SelectSingleNode("/form/item[@name='{0}']".FormatString(name));

			if(node==null) return null;
			return XmlHelper.FilterNull(node.InnerXml);
		}

		/// <summary>
		/// ����name�����ӽڵ��XMLֵ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetInnerXml(string name,DataForm value)
		{
            XmlNode node = this.XmlEle.SelectSingleNode("/form/item[@name='{0}']".FormatString(name));
			if(node==null)
			{
				node=this.XmlDoc.CreateElement("item");
				this.XmlEle.AppendChild(node);
			}
			node.InnerXml=value.XmlEle.OuterXml;
		}


       

		#endregion
	}
}
