using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.Enums;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif

namespace ServerClientShare.PeristenceMessages
{
    public class ServerConfirmedClientMessage : ServerPersistanceMessage<ServerConfirmedClientMessage>
    {
        public string ApprovedMessageId { get; set; }

        public ServerConfirmedClientMessage(string approvedMessageId)
        {
            ApprovedMessageId = approvedMessageId;
            MessageType = NetworkMessageType.ServerConfirmedClientMessage;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(ApprovedMessageId);
            return message;
        }

        public new static ServerConfirmedClientMessage FromMessageArguments(Message message, ref uint offset)
        {
            var approvedMessageId = message.GetString(offset++);
            var dto = new ServerConfirmedClientMessage(approvedMessageId) { };
            return dto;
        }
    }
}
