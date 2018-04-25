using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ClientSentEndedGameMessage : ClientPersistenceMessage<ClientSentEndedGameMessage>
    {
        public string PlayerName { get; set; }

        public ClientSentEndedGameMessage(string playerName)
        {
            MessageType = NetworkMessageType.ClientSentEndedGame;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(Id);
            message.Add(PlayerName);
            return message;
        }

        public new static ClientSentEndedGameMessage FromMessageArguments(Message message, ref uint offset)
        {
            var id = message.GetString(offset++);
            var playerName = message.GetString(offset++);
            var dto = new ClientSentEndedGameMessage(playerName) { Id = id };
            return dto;
        }
    }
}
