
using System;
using System.Collections.Generic;
using DS.AFP.Communication;
using DS.AFP.Communication.DS21;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// �ṩ21��Ϣ�Ĳ���
    /// </summary>
    public class DS21Message : IDS21Message
    {
        private readonly Dictionary<Type, MsgBase> msgs = new Dictionary<Type, MsgBase>();

        private IDS21 ds21 = null;

        public IDS21 DS21
        {
            get { return this.ds21; }
        }

        /// <summary>
        /// �ҽ�DS21
        /// </summary>
        /// <param name="ds21"></param>
        /// <returns></returns>
        public bool Register(IDS21 ds21)
        {
            this.ds21 = ds21;
            return true;
        }

        /// <summary>
        /// ͨ��TMsgDtoType���Ҷ�Ӧ��TMsgDtoType��Msgenvelope
        /// </summary>
        /// <typeparam name="TMsgDtoType"></typeparam>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public Msgenvelope<TMsgDtoType> GetMessage<TMsgDtoType>() where TMsgDtoType : class, new()
        {
            MsgBase existingMessage = null;

            if (!this.msgs.TryGetValue(typeof(TMsgDtoType), out existingMessage))
            {
                //TMsgType newMsg = new TMsgType();
                Msgenvelope<TMsgDtoType> newMsg = new Msgenvelope<TMsgDtoType>();
                this.msgs[typeof(TMsgDtoType)] = newMsg;

                return newMsg;
            }
            else
            {
                return (Msgenvelope<TMsgDtoType>)existingMessage;
            }
        }


        /// <summary>
        /// ͨ��TMsgType���Ҷ�Ӧ��MsgBase
        /// </summary>
        /// <param name="TMsgType"></param>
        /// <returns></returns>
        public object GetMessage(Type TMsgType)
        {
            MsgBase existingMessage = null;

            if (!this.msgs.TryGetValue(TMsgType, out existingMessage))
            {
                return null;
            }
            else
            {
                return existingMessage;
            }
        }

        /// <summary>
        /// ����21��Ϣ
        /// </summary>
        /// <param name="dsMsg">21��Ϣ</param>
        /// <returns></returns>
        public int Send(DSMsg dsMsg)
        {
            if (ds21 != null)
                return ds21.Send(dsMsg);
            else
                throw new NullReferenceException("DS21 object does not exist");
        }

        /// <summary>
        /// ����21��Ϣ
        /// </summary>
        /// <param name="dsMsg">21��Ϣ</param>
        /// <param name="isTransferRoute">�Ƿ�Remote��Ĭ�ϲ���Remote��</param>
        /// <returns></returns>
        public int Send(DSMsg dsMsg, bool isTransferRoute)
        {
            if (ds21 != null)
                return ds21.Send(dsMsg, isTransferRoute);
            else
                throw new NullReferenceException("DS21 object does not exist");
        }
    }
}
