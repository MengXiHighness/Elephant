using DS.AFP.Common.Core.Utility;
using System;
using System.Xml;
using DS.AFP.Common.Core;

namespace DS.AFP.Data
{
	/// <summary>
	/// DataForm 的摘要说明。
	/// </summary>
    [Serializable]
	public class DataForm:DataNode
	{
       

		#region 构造函数

		/// <summary>
		/// 空的DataForm构造函数
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
        /// 根据XML字符串构造DataForm
        /// </summary>
        /// <param name="xmlsrc"></param>
        public DataForm(string xmlsrc)
            : base(xmlsrc)
        {
            this.NodeType = "Form";
        }

		/// <summary>
		/// 根据DataForm构造新的DataForm
		/// </summary>
		/// <param name="xmlsrc"></param>
		public DataForm(DataForm dataForm):base(dataForm.ToString())
		{
            this.NodeType = "Form";
		}
		/// <summary>
		/// 根据XmlElement构造DataForm
		/// </summary>
		/// <param name="ele"></param>
		public DataForm(XmlElement ele):base(ele)
		{
            this.NodeType = "Form";
		}

		/// <summary>
		/// 根据XmlDocument和XmlElement构造DataForm
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="ele"></param>
		internal DataForm(XmlDocument doc,XmlElement ele):base(doc,ele)
		{
            this.NodeType = "Form";
		}

		#endregion


		#region 共有方法
		/// <summary>
		/// 添加子节点
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
		/// 根据name获取子节点的值
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
		/// 根据name设置子节点的值
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
		/// 根据name获取子节点的XML值
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
		/// 根据name设置子节点的XML值
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
