using DS.AFP.Communication.Chain;
using DS.AFP.Communication.SocketNameSpace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Utility
{
    /// <summary>
    /// 提供对文件的上传下载方法
    /// </summary>
    public class NetFileHelper
    {
        ChainClient Client { get; set; }

        FileStream FileStream = null;

        int length = 0;

        int receive_length = 0;


        /// <summary>
        /// 下载文件
        /// <code>
        /// ChainClient client1 = null;
        ///private void Button_Click_5(object sender, RoutedEventArgs e)
        ///{
        ///    receive_length = 0;
        ///    length = 0;
        ///    client1 = new ChainClient("192.168.7.188", 2012);
        ///    //client1.OnReceived += client1_OnReceived;
        ///    byte[] data = Encoding.UTF8.GetBytes("0");
        ///    ChainPackage cp = new ChainPackage(data);
        ///    cp.Key = "LogCommand";
        ///    cp.MsgNo = "S10002";//心跳消息
        ///    cp.Header.Add("Uid", WpfAddinAppModule.Uid);
        ///    NetFileHelper.Download(client1, cp, AppDomain.CurrentDomain.BaseDirectory + "test1111.zip");
        ////}
        /// </code>
        /// </summary>
        /// <param name="client">ChainClient客户端</param>
        /// <param name="chainPackage">ChainPackage消息包</param>
        /// <param name="saveFilePath">文件路径</param>
        /// <param name="isCreate">是否创建文件</param>
        public static void Download(ChainClient client, ChainPackage chainPackage, string saveFilePath, bool isCreate = true)
        {
            new NetFileHelper().DownloadHelper(client, chainPackage, saveFilePath, isCreate);
        }

        void DownloadHelper(ChainClient client, ChainPackage chainPackage, string saveFilePath, bool isCreate = true)
        {
            Client = client;
            FileStream = new FileStream(saveFilePath, FileMode.OpenOrCreate);
            Client.OnReceived += Client_OnReceived;
            Client.Send(chainPackage);
        }

        void Client_OnReceived(object sender, ChainPackage e)
        {
            try
            {
                ChainPackage cp = e;
                string s = Encoding.UTF8.GetString(cp.Body);
                if (length == 0)
                    length = Convert.ToInt32(cp.Header.Data[HeadKeys.FileLength]);

                receive_length += cp.Body.Length;
                FileStream.Write(cp.Body, 0, cp.Body.Length);
                FileStream.Flush();
                if (length <= receive_length)
                {
                    FileStream.Close();
                    Client.Close();
                }
            }
            catch
            {
                FileStream.Close();
                Client.Close();
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="client">ChainClient客户端</param>
        /// <param name="chainPackage">ChainPackage消息包</param>
        /// <param name="saveFilePath">文件路径</param>
        public static void Upload(ChainClient client, ChainPackage chainPackage, string saveFilePath)
        {
            new NetFileHelper().UploadHelper(client, chainPackage, saveFilePath);
        }

        void UploadHelper(ChainClient client, ChainPackage chainPackage, string saveFilePath)
        {
            #region 向监控发送实时文件
            byte[] buff = new byte[800 * 1024];



            using (FileStream fs = File.OpenRead(saveFilePath))
            {
                BinaryReader binaryReader = new BinaryReader(fs);
                int count = binaryReader.Read(buff, 0, buff.Length);
                while (count > 0)
                {
                    byte[] t_data = new byte[count];
                    Buffer.BlockCopy(buff, 0, t_data, 0, count);
                    ChainPackage cp = new ChainPackage(t_data);
                    cp.Header.Data.Clear();
                    cp.Header.Data.Add(chainPackage.Header.Data);

                    cp.Header.Data[HeadKeys.ContentLen] = t_data.Length.ToString();
                    cp.Header.Data.Add(HeadKeys.FileLength, fs.Length.ToString());

                    client.Send(cp,false);

                    buff = new byte[800 * 1024];
                    count = binaryReader.Read(buff, 0, buff.Length);
                }
            }
            #endregion
        }

    }
}
