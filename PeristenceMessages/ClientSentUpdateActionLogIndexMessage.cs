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
    public class ClientSentUpdateActionLogIndexMessage : PersitenceMessage<ClientSentUpdateActionLogIndexMessage>
    {
        public int ActionLogIndex { get; set; }

        public ClientSentUpdateActionLogIndexMessage(int actionLogIndex)
        {
            MessageType = NetworkMessageType.ClientSentActionLogIndex;
            ActionLogIndex = actionLogIndex;
        }

        public override Message ToMessage(Message message)
        {
            message.Add(Id);
            message.Add(ActionLogIndex);
            return message;
        }

        public new static ClientSentUpdateActionLogIndexMessage FromMessageArguments(Message message, ref uint offset)
        {
            var id = message.GetString(offset++);
            var actionLogIndex = message.GetInt(offset++);
            var dto = new ClientSentUpdateActionLogIndexMessage(actionLogIndex) { Id = id };
            return dto;
        }
    }
}
