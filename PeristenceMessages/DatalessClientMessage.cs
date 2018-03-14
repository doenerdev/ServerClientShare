using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class DatalessClientMessage : ClientPersistenceMessage<DatalessClientMessage>
    {
        public DatalessClientMessage(NetworkMessageType type)
        {
            MessageType = type;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            return message;
        }

        public new static DatalessClientMessage FromMessageArguments(Message message, ref uint offset)
        {
            var type = (NetworkMessageType) message.GetInt(offset++);
            return new DatalessClientMessage(type);
        }
    }
}
