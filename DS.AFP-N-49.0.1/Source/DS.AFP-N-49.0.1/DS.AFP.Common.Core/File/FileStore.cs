
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    public class FileStore
    {
        private static FileDB instance = null;

        protected FileStore() { }

        static FileStore()
        {
            if (instance == null)
                instance = new FileDB();
        }

        public static void AddHandler(IHandler newHander)
        {
            instance.AddHandler(newHander);
        }

        public static void AddHandler(IHandler[] newHandlers)
        {
            foreach(IHandler handler in newHandlers)
            {
                instance.AddHandler(handler);
            }
        }

        public static void Write(string handlerName,string content)
        {
            instance.Write(new PersistentData()
            {
                SessionID = handlerName,
                Data = content
            });
        }

        public static void CloseHandler(string handlerName)
        {
            instance.CloseHandler(handlerName);
        }

        public static void CloseAll()
        {
            //instance.
            foreach(var s in instance.SessionHandlerCollection)
            {
                s.Value.Close();
            }
            instance.SessionHandlerCollection.Clear();
        }

        public static bool ContainsHandler(string handlerKey)
        {
            return instance.ContainsHandler(handlerKey);
        }

        //public static string ReadToEnd(string path)
        //{
        //    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        //    StreamReader sr = new StreamReader(fs);
        //    string strline;
        //    return sr.ReadToEnd();
        //}

        //public static StreamReader Read(string path)
        //{
        //    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        //    StreamReader sr = new StreamReader(fs);
        //    return sr;
        //}
    }
}
