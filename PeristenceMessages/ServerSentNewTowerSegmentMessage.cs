using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentNewTowerSegmentMessage : ServerPersistanceMessage<ServerSentNewTowerSegmentMessage>
    {
        public string PlayerName { get; set; }
        public TowerSegmentDTO TowerSegment { get; set; }

        public ServerSentNewTowerSegmentMessage(string playerName, TowerSegmentDTO towerSegment)
        {
            MessageType = NetworkMessageType.ServerSentNewTowerSegment;
            TowerSegment = towerSegment;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(PlayerName);
            message = TowerSegment.ToMessage(message);
            return message;
        }

        public new static ServerSentNewTowerSegmentMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var towerSegment = TowerSegmentDTO.FromMessageArguments(message, ref offset);
            var dto = new ServerSentNewTowerSegmentMessage(playerName, towerSegment);
            return dto;
        }
    }
}
