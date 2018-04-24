using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ClientSentForfeitGameMessage : ClientPersistenceMessage<ClientSentForfeitGameMessage>
    {
        public string PlayerName { get; set; }

        public ClientSentForfeitGameMessage(string playerName)
        {
            MessageType = NetworkMessageType.ClientSentForfeitGame;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(Id);
            message.Add(PlayerName);
            return message;
        }

        public new static ClientSentForfeitGameMessage FromMessageArguments(Message message, ref uint offset)
        {
            var id = message.GetString(offset++);
            var playerName = message.GetString(offset++);
            var dto = new ClientSentForfeitGameMessage(playerName) { Id = id };
            return dto;
        }
    }
}
