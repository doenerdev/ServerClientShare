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
    public class ClientSentChangeTurnMessage : PersitenceMessage<ClientSentChangeTurnMessage>
    {
        public int TurnNumber { get; set; }
        public int NextPlayerIndex { get; set; }

        public ClientSentChangeTurnMessage(int turnNumber, int nextPlayerIndex)
        {
            MessageType = NetworkMessageType.ClientSentChangeTurn;
            TurnNumber = turnNumber;
            NextPlayerIndex = nextPlayerIndex;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(Id);
            message.Add(TurnNumber);
            message.Add(NextPlayerIndex);
            return message;
        }

        public new static ClientSentChangeTurnMessage FromMessageArguments(Message message, ref uint offset)
        {
            var id = message.GetString(offset++);
            var turnNumber = message.GetInt(offset++);
            var nextPlayerIndex = message.GetInt(offset++);
            var dto = new ClientSentChangeTurnMessage(turnNumber, nextPlayerIndex) { Id = id };
            return dto;
        }
    }
}
