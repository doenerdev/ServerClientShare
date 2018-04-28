using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
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
