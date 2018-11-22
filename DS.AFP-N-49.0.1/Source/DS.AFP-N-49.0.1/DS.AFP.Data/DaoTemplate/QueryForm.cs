using DS.AFP.Common.Core.Utility;
using System;
using System.Collections.Generic;
using System.Xml;
using DS.AFP.Common.Core;

namespace DS.AFP.Data
{

    [Serializable]
	public class QueryForm:DataForm
	{
		public QueryForm()
		{
		}

        public QueryForm(string xmlString)
            : base(xmlString)
        {
        }

        public IList<DataItem> Condition
        {
            get
            {
                IList<DataItem> dis = new List<DataItem>();
                XmlNodeList xmlnodes = this.XmlEle.SelectNodes("/form/item[not (@name)]");
                foreach (XmlNode node in xmlnodes)
                {
                    dis.Add(new DataItem(this.XmlDoc, (XmlElement)node));
                }
                return dis;
            }
        }

        public Pager Pager
        {
            get
            {
                string pagerStr = this.XmlEle.Attributes["Pager"].Value;
                if (pagerStr.IsNullOrEmpty())
                    return null;
                string[] t = pagerStr.Split(',');
                Pager pager = new Pager();
                foreach(string t1 in t)
                {
                    string[] t2 = t1.Split(':');
                    switch(t2[0].ToLower())
                    {
                        case "cp":
                            pager.CurrentPagte = Convert.ToInt32(t2[1]);
                            break;
                        case "tp":
                            pager.TotalPage = Convert.ToInt32(t2[1]);
                            break;
                        case "ps":
                            pager.PageSize = Convert.ToInt32(t2[1]);
                            break;
                    }
                }
                return pager;
            }
        }

        public string Order
        {
            get
            {
                string orderStr = this.XmlEle.Attributes["Order"].Value;
                return orderStr;
            }
        }

        public string Fields
        {
            get
            {
                string fieldsStr = XmlHelper.FilterNull(this.XmlEle.Attributes["fields"].Value);
                if (fieldsStr == "")
                    fieldsStr = " * ";
                return fieldsStr;
            }
        }

		
	}
}
