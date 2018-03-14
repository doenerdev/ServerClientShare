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
    public class ServerSentActionMessage : ServerPersistanceMessage<ServerSentActionMessage>
    {
        public string ActionName { get; set; }
        public string PlayerName { get; set; }
        public string ActionJson { get; set; }

        public ServerSentActionMessage(string actionName, string playerName, string actionJson)
        {
            MessageType = NetworkMessageType.ServerSentGameAction;
            ActionName = actionName;
            PlayerName = playerName;
            ActionJson = actionJson;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(ActionName);
            message.Add(PlayerName);
            message.Add(ActionJson);
            return message;
        }

        public new static ServerSentActionMessage FromMessageArguments(Message message, ref uint offset)
        {
            var actionName = message.GetString(offset++);
            var playerName = message.GetString(offset++);
            var actionJson = message.GetString(offset++);
            var dto = new ServerSentActionMessage(actionName, playerName, actionJson);
            return dto;
        }
    }
}
