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
    public class ServerSentForfeitGameMessage : ServerPersistanceMessage<ServerSentForfeitGameMessage>
    {
        public string PlayerName { get; set; }

        public ServerSentForfeitGameMessage(string playerName)
        {
            MessageType = NetworkMessageType.ServerSentForfeitGame;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(Id);
            message.Add(PlayerName);
            return message;
        }

        public new static ServerSentForfeitGameMessage FromMessageArguments(Message message, ref uint offset)
        {
            var id = message.GetString(offset++);
            var playerName = message.GetString(offset++);
            var dto = new ServerSentForfeitGameMessage(playerName) { Id = id};
            return dto;
        }
    }
}
