using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentMarketplaceMessage : ServerPersistanceMessage<ServerSentMarketplaceMessage>
    {
        public string PlayerName { get; set; }
        public DeckDTO Marketplace { get; set; }

        public ServerSentMarketplaceMessage(string playerName, DeckDTO marketplace)
        {
            MessageType = NetworkMessageType.ServerSentMarketplace;
            Marketplace = marketplace;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(PlayerName);
            message = Marketplace.ToMessage(message);
            return message;
        }

        public new static ServerSentMarketplaceMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var marketplace = DeckDTO.FromMessageArguments(message, ref offset);
            var dto = new ServerSentMarketplaceMessage(playerName, marketplace);
            return dto;
        }
    }
}
