using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    class ServerSentHexMapMessage : ServerPersistanceMessage<ServerSentHexMapMessage>
    {
        public string PlayerName { get; set; }
        public HexMapDTO HexMap { get; set; }

        public ServerSentHexMapMessage(string playerName, HexMapDTO hexMap)
        {
            MessageType = NetworkMessageType.ServerSentHexMap;
            HexMap = hexMap;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(PlayerName);
            message = HexMap.ToMessage(message);
            return message;
        }

        public new static ServerSentHexMapMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var hexMap = HexMapDTO.FromMessageArguments(message, ref offset);
            var dto = new ServerSentHexMapMessage(playerName, hexMap);
            return dto;
        }
    }
}
