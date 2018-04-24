using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
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
            message.Add(PlayerName);
            return message;
        }

        public new static ServerSentForfeitGameMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var dto = new ServerSentForfeitGameMessage(playerName);
            return dto;
        }
    }
}
