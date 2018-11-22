using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 序列化扩展（支持XML序列化，Json序列化，Soap，binary序列化以及各自的反序列化）
    /// </summary>
    public static class SerializeExtensions
    {
        #region XML序列化
        /// <summary>
        /// 文件化XML序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void SerializeToXmlFile(this object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文件化XML反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromXmlFile<T>(this string filename) where T : class
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        /// <returns>序列化xml串</returns>
        public static string SerializeToXml(this object item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, item);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromXml<T>(this string str) where T : class
        {
            return (T)DeserializeFromXml(str, typeof(T));
        }

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        /// <returns>反序列化后的对象（object对象）</returns>
        public static object DeserializeFromXml(this string str, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return serializer.Deserialize(reader);
            }
        }

       
        #endregion

        #region Json序列化
        /// <summary>
        /// JsonSerializer序列化
        /// </summary>
        /// <param name="item">对象</param>
        /// <returns>序列化后的string</returns>
        public static string SerializeToJson(this object item)
        {
            #region 微软标准方法
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string jsonString = js.Serialize(item);

            //jsonString = Regex.Replace(jsonString, @"\\/Date\((\d+)\)\\/", match =>
            //{
            //    DateTime dt = new DateTime(1970, 1, 1);
            //    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
            //    dt = dt.ToLocalTime();
            //    return dt.ToString("yyyy-MM-dd HH:mm:ss");
            //});
            //return jsonString;
            #endregion 

            //第三方方法，大数据量效率高
            //string jsonString = fastJSON.JSON.Instance.ToJSON(item);
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string jsonString = JsonConvert.SerializeObject(item, Newtonsoft.Json.Formatting.Indented, timeFormat);


            return jsonString;
        }

        /// <summary>
        /// JsonSerializer反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromJson<T>(this string str) where T : class
        {
            #region 微软标准方法
            //JavaScriptSerializer js = new JavaScriptSerializer();

            //return js.Deserialize<T>(str);
            #endregion

            //return (T)fastJSON.JSON.Instance.ToObject(str);

            return JsonConvert.DeserializeObject<T>(str);

        }

        public static object DeserializeFromJson(this string str, Type type) 
        {
            #region 微软标准方法
            //JavaScriptSerializer js = new JavaScriptSerializer();

            //return js.Deserialize<T>(str);
            #endregion

            //return (T)fastJSON.JSON.Instance.ToObject(str);

            return JsonConvert.DeserializeObject(str,type);

        }
        #endregion

        #region SoapFormatter序列化
        /// <summary>
        /// SoapFormatter序列化
        /// </summary>
        /// <param name="item">对象</param>
        /// <returns>序列化后的string</returns>
        public static string SerializeToSoap(this object item)
        {
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

        /// <summary>
        /// SoapFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromSoap<T>(this string str)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);
            SoapFormatter formatter = new SoapFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                xmlDoc.Save(ms);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion

        #region BinaryFormatter序列化
        /// <summary>
        /// BinaryFormatter序列化
        /// </summary>
        /// <param name="item">对象</param>
        /// <returns>序列化后的string</returns>
        public static string SerializeToBinary(this object item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                byte[] bytes = ms.ToArray();
                StringBuilder sb = new StringBuilder();
                foreach (byte bt in bytes)
                {
                    sb.Append(string.Format("{0:X2}", bt));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        /// <returns>反序列化后的对象</returns>
        public static T DeserializeFromBinary<T>(this string str)
        {
            int intLen = str.Length / 2;
            byte[] bytes = new byte[intLen];
            for (int i = 0; i < intLen; i++)
            {
                int ibyte = Convert.ToInt32(str.Substring(i * 2, 2), 16);
                bytes[i] = (byte)ibyte;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion
    }
}
