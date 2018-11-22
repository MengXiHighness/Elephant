using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.Reflection;
using System.Net.NetworkInformation;
using System.IO;
using System.Text.RegularExpressions;
using System.ServiceModel;

using System.ServiceModel.Channels;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using System.Net;
using DS.AFP.Common.Core.Utility;

namespace DS.AFP.Common.Core
{


    public class LogTraceClient
    {
        static string ServicesUrl = string.Empty;
        static readonly object m_eventLock = new object();
        //public static Action<string> LogAction
        //{
        //    get;
        //    set;
        //}


        //static EventHandler<LogTraceInfo> logEvent;
        //public static event EventHandler<LogTraceInfo> LogEvent
        //{
        //    //显式实现'add'方法
        //    add
        //    {
        //        //加私有锁，并向委托链表增加一个处理程序(以'value'为参数)
        //        lock (m_eventLock) { logEvent += value; }
        //    }
        //    //显式实现'remove'方法
        //    remove
        //    {
        //        //加私有锁，并从委托链表从中移除处理程序(以'value'为参数)
        //        lock (m_eventLock) { logEvent -= value; }
        //    }
        //}


        static bool isClosed
        {
            get;
            set;
        }



        static string GetFileData(string fileName, System.Text.Encoding ed)
        {
            string data = "";
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, ed == null ? System.Text.Encoding.UTF8 : ed))
                {
                    data = sr.ReadToEnd();
                    sr.Close();
                }
                fs.Close();
            }
            return data;
        }




        public static void WriteLog(string level, string message)
        {
            try
            {
                //if (LogAction != null)
                //    LogAction(message);

                LogTraceInfo logTraceInfo = new LogTraceInfo() { Level = level, Message = message };
                //logEvent(null, logTraceInfo);

                //if (ServicesUrl.IsNullOrEmpty())
                //{
                //    DsConfigurationManager dscm = new DsConfigurationManager();
                //    IDsConfigurationSection config = dscm.DsRootConfigurationSection;
                //    if (!config.Trace)
                //        return;
                //    ServicesUrl = config.Communications["dsLogTrace"].Address;
                //}



                //HttpHelper.Post(ServicesUrl, logTraceInfo.SerializeToJson());
            }
            catch (Exception ex)
            {

            }
        }

        public static void WriteLog(string level, Exception ex)
        {
            WriteLog(level, ex.Message + "\r\n" + ex.StackTrace);
        }

    }

    public class LogTraceType
    {
        public const string Info = "Info";
        public const string Error = "Error";
        public const string Debug = "Debug";
        public const string Warn = "Warn";
        public const string FATAL = "Fatal";
    }


}
