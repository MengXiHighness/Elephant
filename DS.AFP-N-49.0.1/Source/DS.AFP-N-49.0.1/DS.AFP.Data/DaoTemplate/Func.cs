using System;
using System.Xml;
namespace DS.AFP.Data
{
	/// <summary>
	/// Func 的摘要说明。
	/// </summary>
	public class Func
	{
		private Func(){}
		public static void CopyXmlAttrs(XmlElement fromNode,XmlElement toNode)
		{
			XmlAttributeCollection atrs= fromNode.Attributes;
			for(int i=0;i<atrs.Count;i++)
				toNode.SetAttribute(atrs.Item(i).Name,atrs.Item(i).Value);
		}

		public static bool AttrIsNull(string attr)
		{	if(attr == null || attr == "")
				return true;
			else
				return false;
		}

		public static string FullKey(string key)
		{
			int pos=key.IndexOf(".");
			string system,name;
			if(pos>0)
			{
				system = key.Substring(0,pos);
				name =key.Substring(pos+1);
			}
			else
			{
				system =  "System";
				name = key;
			}
			return system+"."+name;
			

			//刘立，为了解决没有在EnumKey中加入'.'，而造成自动加入前缀,导致无法取到枚举的问题,2004-11-8
			//return key;
		}


		
		
		public static bool ToBool(string value)
		{	string val = value.ToLower();
			if(val=="t"||val=="true"||val=="on"||val=="yes")
				return true;
			else
				return false;
		}
		public static int ByteLen(string str)
		{
			char[] chs = str.ToCharArray();
			int len=0;
			for(int i=0;i<chs.Length;i++)
			{
				if((int)chs[i]>256)
					len+=2;
				else
					len++;
			}
			return len;
		}

		/// <summary>
		/// 反转base64编码
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string Decode64(string input)
		{
			const string keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
			string output = "";
			int chr1,chr2,chr3;
			int enc1,enc2,enc3,enc4;
			int i = 0;
			char[] _input = input.ToCharArray();

			do 
			{
				enc1 = keyStr.IndexOf(_input[i++]);
				enc2 = keyStr.IndexOf(_input[i++]);
				enc3 = keyStr.IndexOf(_input[i++]);
				enc4 = keyStr.IndexOf(_input[i++]);

				chr1 = (enc1 << 2) | (enc2 >> 4);
				chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
				chr3 = ((enc3 & 3) << 6) | enc4;

				output = output + (char)chr1;

				if (enc3 != 64)
				{
					output = output + (char)chr2;
				}

				if (enc4 != 64) 
				{
					output = output + (char)chr3;
				}
				chr1=chr2=chr3=0;
				enc1=enc2=enc3=enc4=0;
			} while (i < input.Length);
			return output;
		}

	}
}
