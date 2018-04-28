using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif 
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentChangeTurnMessage : ServerPersistanceMessage<ServerSentChangeTurnMessage>
    {
        public string PlayerName { get; set; }

        public ServerSentChangeTurnMessage(string playerName)
        {
            MessageType = NetworkMessageType.ServerSentChangeTurn;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(PlayerName);
            return message;
        }

        public new static ServerSentChangeTurnMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var dto = new ServerSentChangeTurnMessage(playerName);
            return dto;
        }
    }
}
