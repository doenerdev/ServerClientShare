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
    public class ClientSentUpdateMatchMessage : PersitenceMessage<ClientSentUpdateMatchMessage>
    {
        public int TurnNumber { get; set; }
        public MatchDTO Match { get; set; }

        public ClientSentUpdateMatchMessage(int turnNumber, MatchDTO match)
        {
            MessageType = NetworkMessageType.ClientSentMatch;
            TurnNumber = turnNumber;
            Match = match;
        }

        public override Message ToMessage(Message message)
        {
            message.Add(Id);
            message.Add(TurnNumber);
            message = Match.ToMessage(message);
            return message;
        }

        public new static ClientSentUpdateMatchMessage FromMessageArguments(Message message, ref uint offset)
        {
            var id = message.GetString(offset++);
            var turnNumber = message.GetInt(offset++);
            var match = MatchDTO.FromMessageArguments(message, ref offset);
            var dto = new ClientSentUpdateMatchMessage(turnNumber, match) { Id = id};
            return dto;
        }
    }
}
