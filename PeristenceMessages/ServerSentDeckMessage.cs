using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentDeckMessage : ServerPersistanceMessage<ServerSentDeckMessage>
    {
        public string PlayerName { get; set; }
        public DeckDTO Deck { get; set; }

        public ServerSentDeckMessage(string playerName, DeckDTO deck)
        {
            MessageType = NetworkMessageType.ServerSentDeck;
            Deck = deck;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(PlayerName);
            message = Deck.ToMessage(message);
            return message;
        }

        public new static ServerSentDeckMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var deck = DeckDTO.FromMessageArguments(message, ref offset);
            var dto = new ServerSentDeckMessage(playerName, deck);
            return dto;
        }
    }
}
