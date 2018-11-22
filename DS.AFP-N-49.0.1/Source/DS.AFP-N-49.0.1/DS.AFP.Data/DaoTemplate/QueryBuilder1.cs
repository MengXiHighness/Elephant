using System;
using System.Xml;

namespace DS.AFP.Data
{
	

	public class QueryBuilder
	{
		private DataForm qryData;
		private DataForm qryTmpl;

		#region 构造函数

		public QueryBuilder(DataForm qryTmpl,DataForm qryData)
		{
			this.qryData = qryData;
			this.qryTmpl = qryTmpl;
		}

		public QueryBuilder(string qrySchema,DataForm qryData)
		{
			this.qryData = qryData;
			this.qrySchema = new XmlDocument();
			this.qrySchema.LoadXml(qrySchema);
		}

		
		private void InitSchema()
		{
		}
		#endregion

		#region 属性接口

		public DataForm QueryData
		{
			get
			{
					return this.qryData;
			}
		}
		public XmlDocument QuerySchema
		{
			get
			{
					return this.qrySchema;
			}
		}
		public string Name
		{
			get
			{
				return this.qryData.GetAttr("Name");
			}set
			 {
				 this.qryData.SetAttr("Name",value);
			 }
		}
		public string Logic
		{
			get
			{
				return this.qryData.GetAttr("Logic");
			}set
			 {
				 this.qryData.SetAttr("Logic",value);
			 }
		}
		public string PageIndex
		{
			get
			{
				return this.qryData.GetAttr("PageIndex");
			}set
			 {
				 this.qryData.SetAttr("PageIndex",value);
			 }
		}

		public string PageAction
		{
			get
			{
				return this.qryData.GetAttr("PageAction");
			}set
			 {
				 this.qryData.SetAttr("PageAction",value);
			 }
		}
		public string OrderBy
		{
			get
			{
					return this.qryData.GetAttr("OrderBy");
			}set
			 {
					 this.qryData.SetAttr("OrderBy",value);
			 }
		}

		public string this[string qryDataField]
		{
			get
			{
					return this.qryData.GetValue(qryDataField);
			}set
			 {
					 this.qryData.SetValue(qryDataField,value);
			 }
		}


		#endregion


		public string GetOrderByString()
		{
				return this.GetOrderByString(false);
		}
		public string GetOrderByString(bool hasPrefix)
		{
				if(this.qryData==null)return "";

			string odr = this.qryData.GetAttr("OrderBy");
			string perfix="";
			if(hasPrefix)
				perfix = "ORDER BY ";
			if(odr == null)
				return "";
			StringParam sp = new StringParam(odr);
			string ordby="";
			for(int i=0;i<sp.Count;i++)
				ordby += sp.Keys[i] + " " + sp[i] + ",";
			if(ordby == "")
				return "";
			else
				return  perfix+ ordby.Substring(0,ordby.Length-1);
		}

		public PageIndex GetPageIndex()
		{	
			if(this.qryData==null)return null;
			string pistr = this.qryData.GetAttr("PageIndex");
			if(Func.AttrIsNull(pistr))
				return null;
			PageIndex pi= new PageIndex(pistr);
			string action = this.qryData.GetAttr("PageAction");
			if(Func.AttrIsNull(action ))
				return pi;
			string jumpNo = this.qryData.GetAttr("JumpNo");
			if(Func.AttrIsNull(jumpNo ))
				pi.SetAction(action,0);
			else
				pi.SetAction(action,Convert.ToInt32(jumpNo));
			return pi;
		}

	
		public string GetSimpleCountSql(string tableAndFields)
		{
				return DbTool.GetCountSql(tableAndFields,this.GetSimpleWhereString());
		}
		public string GetSimpleSql(string tableAndFields)
		{
				return DbTool.GetSelectSql(tableAndFields,this.GetSimpleWhereString(),this.GetOrderByString());
		}

		public string GetSimpleWhereString()
		{
				return this.GetSimpleWhereString(false);
		}

		public string GetSimpleWhereString(bool hasPrefix)
		{	
			if(this.qryData==null||this.qryTmpl==null)return "";
			string perfix="";
			if(hasPrefix)
				perfix = "WHERE ";
			int count = this.qryData.GetItemCount();
			string whereStr="";
			string logic = this.qryTmpl.GetAttr("Logic");
			if(Func.AttrIsNull(logic))
				logic = " AND ";
			logic = logic.ToUpper();




			string fldgrp = this.qryTmpl.GetAttr("FieldGroup");
			if (Func.AttrIsNull(fldgrp))
			{
				for(int i=0;i<count;i++)
				{
					DataItem didata = this.qryData.GetItem(i);
					DataItem ditmpl = this.qryTmpl.GetItem(didata.GetName());
					if(ditmpl == null)continue; //模板中没有定义
					string name = ditmpl.GetAttr("InnerName");
					if(Func.AttrIsNull(name))
						name = ditmpl.GetName();
					if((didata.GetValue().Trim()!="" || 
						ditmpl.GetAttr("Match").ToLower()=="notnull" || 
						ditmpl.GetAttr("Match").ToLower()=="isnull") && 
						ditmpl.GetAttr("Auto").ToLower()!="false" )

						whereStr += "("+QueryBuilder.GetMatch(name,ditmpl.GetAttr("Match"),didata.GetValue())+")"+logic;
				}			
				if(whereStr == "")
					return "";
				else
					return perfix+whereStr.Substring(0,whereStr.Length-logic.Length);            
			}
			else
			{
				string[] grpary = fldgrp.Split(';');//条件分组
				for(int j=0;j<grpary.Length;j++)
				{
					if (grpary[j].Trim().Length<=0)
						continue;
					string[] tmpary = grpary[j].Split(':');
					if (whereStr == "")
						whereStr += this.GetGroupWhereString(tmpary[1].Split(','),tmpary[0]);//添加组内的逻辑
					else
						whereStr += logic+ this.GetGroupWhereString(tmpary[1].Split(','),tmpary[0]);//添加组内的逻辑
				}
			}
			if(whereStr == "")
				return "";
			else
				return perfix+whereStr;
		}

		private string GetGroupWhereString(string[] grpFields,string logic)
		{
			int count = grpFields.Length;
			string whereStr="";
			for(int i=0;i<count;i++)
			{
				DataItem didata = this.qryData.GetItem(grpFields[i]);
				DataItem ditmpl = this.qryTmpl.GetItem(didata.GetName());
				if(ditmpl == null)continue; //模板中没有定义
				string name = ditmpl.GetAttr("InnerName");
				if(Func.AttrIsNull(name))
					name = ditmpl.GetName();
				if(didata.GetValue().Trim()!="" && ditmpl.GetAttr("Auto").ToLower()!="false")
				{
						if(whereStr=="")
						whereStr += "("+QueryBuilder.GetMatch(name,ditmpl.GetAttr("Match"),didata.GetValue())+")";
					else
						whereStr += logic + "("+QueryBuilder.GetMatch(name,ditmpl.GetAttr("Match"),didata.GetValue())+")";
				}
			}
			// 如果whereStr==""，就会出现返回值为"()"，然后如果在后面加" AND Xx='yy'"，就会变成"() AND Xx='yy'"
			// 修改者：伍子欣
			if (whereStr != "")
				return "(" +whereStr+ ")";
			else
				return "";
		}

	
		public string GetSchemaCountSql()
		{
			return null;
		}

		public string GetSchemaWhereString()
		{
				return null;
		}
		public string GetSchemaWhereString(bool hasPrefix)
		{
			return null;
		}

		public string GetSchemaSql()
		{	
			return null;
		}




		#region 添加QuerySchema

		public void AddSchemaSelectTable(string tableName,string fieldsName ,Boolean isMaster)
		{
				this.AddSchemaSelectTable(tableName,fieldsName,isMaster,null);
		}

		public void AddSchemaSelectTable(string tableName,string fieldsName ,Boolean isMaster,string joinCondition)
		{
			InitSchema();
			XmlElement tables = (XmlElement)this.qrySchema.SelectSingleNode("/QuerySchema/SelectTable");
			XmlElement newTable = this.qrySchema.CreateElement("Table");
			tables.AppendChild(newTable);
			newTable.SetAttribute("Name",tableName);
			newTable.SetAttribute("Fields",fieldsName);
			newTable.SetAttribute("IsMaster",isMaster.ToString());
			if(Func.AttrIsNull(joinCondition))
				newTable.SetAttribute("Join",joinCondition);
		}

		public void AddSchemaCondtionField(string fieldName,string matchString)
		{	
			this.AddSchemaCondtionField(fieldName,matchString,null,null,false,false,false,false);
		}
		public void AddSchemaCondtionField(string fieldName,string matchString,string dataType)
		{	
			this.AddSchemaCondtionField(fieldName,matchString,dataType,null,false,false,false,false);
		}	
		
		public void AddSchemaCondtionField(string fieldName,string matchString,string dataType,string innerName)
		{	
			this.AddSchemaCondtionField(fieldName,matchString,dataType,innerName,false,false,false,false);
		}

		public void AddSchemaCondtionField(string fieldName,string matchString,string dataType,string innerName,bool allowEmpty,bool allowMulti,bool isNOT,bool superLike)
		{	
			InitSchema();
			XmlElement fields = (XmlElement)this.qrySchema.SelectSingleNode("/QuerySchema/SelectCondition/Fields");
			XmlElement newField = this.qrySchema.CreateElement("Field");
			fields.AppendChild(newField);
			newField.SetAttribute("Name",fieldName);
			newField.SetAttribute("Match",matchString);
			if(Func.AttrIsNull(dataType))
				newField.SetAttribute("DataType",dataType);
			if(Func.AttrIsNull(innerName))
				newField.SetAttribute("InnerName",innerName);
			if(allowEmpty)
				newField.SetAttribute("CancelEmpty","true");
			if(allowMulti)
				newField.SetAttribute("AllowMulti","true");
			if(isNOT)
				newField.SetAttribute("IsNOT","true");
			if(superLike)
				newField.SetAttribute("SuperLike","true");
		}

		public void AddSchemaCondtionGroup(string logic,string fieldsString)
		{
			InitSchema();
			XmlElement groups = (XmlElement)this.qrySchema.SelectSingleNode("/QuerySchema/SelectCondition/Groups");
			XmlElement newGroup = this.qrySchema.CreateElement("Group");
			groups.AppendChild(newGroup);
			newGroup.SetAttribute("Logic",logic);
			newGroup.SetAttribute("Fields",fieldsString);
		}

		public void AddSchemaSelectOrderBy(string fieldName,string direction)
		{
			InitSchema();
			XmlElement orders = (XmlElement)this.qrySchema.SelectSingleNode("/QuerySchema/SelectOrderBy");
			XmlElement newOrder = this.qrySchema.CreateElement("OrderBy");
			orders.AppendChild(newOrder);
			newOrder.SetAttribute("Field",fieldName);
			newOrder.SetAttribute("Direction",direction);
		}

		#endregion
		


		/// <summary>
		/// 通过查询模板的定义获得缺省得查询DataForm
		/// </summary>
		/// <param name="qryTmpl"></param>
		/// <returns></returns>
		public static DataForm GetDefaultForm(DataForm qryTmpl)
		{
				if(qryTmpl == null)
				return null;
			DataForm df = new DataForm(qryTmpl.ToString());
			df.Clear();
			int count=qryTmpl.GetItemCount();
			for(int i=0;i<count;i++)
			{
					DataItem di = qryTmpl.GetItem(i);
				df.AddItem(di.GetName(),di.GetValue());
			}
			return df;
		}

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
