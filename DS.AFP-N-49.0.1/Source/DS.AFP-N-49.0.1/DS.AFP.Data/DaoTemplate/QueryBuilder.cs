using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Linq;
using DS.AFP.Common.Core;
using System.Collections.ObjectModel;
using DS.AFP.Common.Core.Reflect;
using System.Xml.Serialization;

namespace DS.AFP.Data
{
	
    [Serializable]
	public class QueryBuilder
	{
		private object qryData;
        private QueryForm qryTmpl;
        private QueryForm qryform;

		#region 构造函数

	
        public  QueryBuilder(QueryForm qryTmpl, object qryData)
        {
            this.qryData = qryData;
            this.qryTmpl = qryTmpl;
            Init();
        }

        private void Init()
        {
            qryform = new QueryForm(qryTmpl.CloneNode(true).OuterXml);
            PropertyInfo[] dataprop = qryData.GetType().GetProperties();
            IList<DataItem> conditions = qryform.GetItems();
            foreach (DataItem di in conditions)
            {
                string name = di.GetAttr("name");
                if (name.IsNullOrEmpty())
                {
                    DataForm df = di.GetChildDataForm();
                    IList<DataItem> dis =df.GetItems();
                    foreach(DataItem di2 in dis)
                    {
                        name = di2.GetAttr("name");
                        var prop = dataprop.FirstOrDefault(p1 => p1.Name == name);
                        if (prop == null)
                        {
                            df.Remove(di2);
                            continue;

                        }
                        di2.SetAttr("value", prop.FastGetValue(qryData).ToString());
                        di2.SetAttr("dataType", prop.PropertyType.FullName);
                    }
                }
                else
                {
                    var prop = dataprop.FirstOrDefault(p1 => p1.Name == name );
                    if (prop == null)
                    {
                        qryform.Remove(di);
                        continue;
 
                    }
                    di.SetAttr("value", prop.FastGetValue(qryData).ToString());
                    di.SetAttr("dataType", prop.PropertyType.FullName);
                }
            }
        }

      
		
		#endregion

		#region 属性接口
       

        /// <summary>
        /// 查询实例
        /// </summary>
        public QueryForm QueryForm
        {
            get
            {
                return this.qryform;
            }
            set
            {
                this.qryform = value;
            }
        }


		#endregion



        /// <summary>
		/// 根据匹配域，匹配方法，匹配域值取得匹配条件串
		/// </summary>
		/// <param name="fieldName">匹配域名称(同数据库一致)</param>
		/// <param name="match">匹配方法(null=EQ)</param>
		/// <param name="fieldValue">匹配域的值</param>
		/// <returns>域的条件串</returns>
		public static string GetMatch(string fieldName,string match,string fieldValue)
		{
			string mop="=";
			fieldValue = fieldValue.Replace("'","''");
			if(Func.AttrIsNull(match))
				match = "EQ";
			switch(match.ToUpper())
			{
				case "EQ":	
					mop = fieldName + "='" + fieldValue + "'";	
					break;
				case "UE":	
					mop = fieldName + "<>'" + fieldValue + "'";	
					break;
				case "GT":
					mop = fieldName + ">'" + fieldValue + "'";	
					break;
				case "LT":	
					mop = fieldName + "<'" + fieldValue + "'";	
					break;
				case "IN":	
					string[] flds=fieldValue.Split(',');
					string fldstr="";
					for(int i=0;i<flds.Length;i++)
						fldstr+="'"+flds[i]+"',";
					fldstr = fldstr.Substring(0,fldstr.Length-1);
					mop = fieldName + " IN (" + fldstr + ")";	
					break;
				case "FR":
					mop = fieldName + ">='" + fieldValue + "'";	
					break;
				case "TO":
					if(fieldValue.IndexOf(":")==-1&&fieldValue.IndexOf("-")!=-1)//edit by shawn for to_date include 2007-4-14
						fieldValue += " 23:59:59";
					mop = fieldName + "<='" + fieldValue + "'";	
					break;
				case "LK":
					mop = fieldName + " LIKE '%" + fieldValue + "%'";	
					break;
				case "SK":
					string[] flds0=fieldValue.Split(' ');
					string fldstr0="%";
					for(int i=0;i<flds0.Length;i++)
						if(flds0[i].Trim()!="")
							fldstr0+=flds0[i]+"%";
					//fldstr0 = fldstr0.Substring(0,fldstr0.Length-1);
					mop = fieldName + " LIKE '" + fldstr0 + "'";	
					break;
				case "NOTNULL":
					mop = fieldName + " IS NOT NULL and "+fieldName + " <>'' ";	
					break;
				case "ISNULL":
					mop = fieldName + " IS NULL or "+fieldName + " ='' ";	
					break;
				case "OR":			//支持Like运算，不支持比较符号	刘立 2005-7-14
					string[] flds1=fieldValue.Split(' ');
					string fldstr1="";
					for(int i=0;i<flds1.Length;i++)
						if(flds1[i].Trim()!="")
							fldstr1+=fieldName + " Like '%"+flds1[i]+"%' or ";
					fldstr1 = fldstr1.Substring(0,fldstr1.Length-3);
					mop =" (" + fldstr1 + ") ";	
					break;
				case "IGNORE":
					mop = " 1=1 ";	
					break;
			}
			return mop;
		}

	}
}
