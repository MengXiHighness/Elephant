
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DSNETCTRLLib;
using DS.AFP.Common.Core;

namespace DS.AFP.Communication.DS21
{
    public class DS21:IDS21,IDisposable
    {
        private DSNetCtrlEx dsNetCtrlEx = null;
        public DS21(string ds21ConfigPath)
        {
            if(!File.Exists(ds21ConfigPath))
                throw new FileNotFoundException("DS21配置文件不存在。目录位置：{0}".FormatString(ds21ConfigPath));
            this.dsNetCtrlEx = new DSNetCtrlEx();
            this.dsNetCtrlEx.OnNewMessage += new _IDSNetCtrlExEvents_OnNewMessageEventHandler(dsNetCtrlEx_OnNewMessage);

            short result = 0;
            try
            {
               
                // 将通信配置文件目录强制设为当前应用程序所在目录
                result = this.dsNetCtrlEx.LoadLibrary(ds21ConfigPath, 1);
            }
            catch (Exception e)
            {
                throw new DS21InitializeException("DS21初始化配置文件时异常,函数LoadLibrary返回状态{0}".FormatString(result), e);
            }

        }

        void dsNetCtrlEx_OnNewMessage(int nMsgCount)
        {
            int count = nMsgCount;
            ushort nSrcType, nSrcID, nDestType, nDestID, nMsgType, nMsgID;
            string strData;
            uint nSerialNo, nReserved;
            while (count > 0)
            {
                try
                {
                    int i = InteralDS21.GetNewMessageEx(out nSrcType, out nSrcID, out nDestType, out nDestID,
                        out nMsgType, out nSerialNo, out nReserved, out nMsgID, out strData);
                    DSMsg dsMsg = new DSMsg() {
                        DestID = nDestID,
                        DestType = nDestType,
                        MsgID = nMsgID,
                        MsgType = nMsgType,
                        Reserved = nReserved,
                        ScrID = nSrcID,
                        ScrType = nSrcType,
                        SerialNo = nSerialNo,
                        StrData = strData
                    };
                    switch(dsMsg.DSMessageCode)
                    {
                        case DSMessageCode.SystemGeneral:
                            {
                                break;
                            }
                        case DSMessageCode.SystemXml:
                            {
                                break;
                            }
                        case DSMessageCode.SystemInternal:
                            {
                                break;
                            }
                    }
                
                }
                catch (Exception e)
                {
                }
            }
        }

        public DSNetCtrlEx InteralDS21
        {
            get
            {
                return this.dsNetCtrlEx;
            }
        }


        public void Dispose()
        {
            InteralDS21.FreeLibrary();
        }

        public int Send(DSMsg dsMsg)
        {
            int result = (int)InteralDS21.DSSendData((ushort)dsMsg.ScrType, dsMsg.ScrID,
                 (ushort)dsMsg.DestType, dsMsg.DestID,
                 0, 0, dsMsg.SerialNo, dsMsg.Reserved, dsMsg.MsgID, (uint)dsMsg.StrData.Length, dsMsg.StrData);
            return result;
        }

        public event EventHandler<DSMsg> MessageReceived;

        void IDS21.Send(DSMsg dsMsg)
        {
            throw new NotImplementedException();
        }

        public bool Register(int nodeType, int nodeID)
        {
            try
            {
                short result = 0;
                int nStatus = 1;
                result = InteralDS21.DSRegSvr(Convert.ToUInt16(nodeType),
                   Convert.ToUInt16(nodeID), 0, "none", "", (short)nStatus);
                if (result == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
               throw  new Exception("DS21注册失败异常", e);
               return false;
            }
        }
    }
}
