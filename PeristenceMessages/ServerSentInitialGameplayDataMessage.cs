using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    class ServerSentInitialGameplayDataMessage : ServerPersistanceMessage<ServerSentInitialGameplayDataMessage>
    {
        public string PlayerName { get; set; }
        public InitialGameplayDataDTO Data { get; set; }

        public ServerSentInitialGameplayDataMessage(string playerName, InitialGameplayDataDTO data)
        {
            MessageType = NetworkMessageType.ServerSentInitialGameplayData;
            Data = data;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(PlayerName);
            message = Data.ToMessage(message);
            return message;
        }

        public new static ServerSentInitialGameplayDataMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var data = InitialGameplayDataDTO.FromMessageArguments(message, ref offset);
            var dto = new ServerSentInitialGameplayDataMessage(playerName, data);
            return dto;
        }
    }
}
