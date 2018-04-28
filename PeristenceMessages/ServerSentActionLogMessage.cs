using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentActionLogMessage : ServerPersistanceMessage<ServerSentActionLogMessage>
    {
        public string PlayerName { get; set; }
        public PlayerActionsLogDTO ActionLog { get; set; }

        public ServerSentActionLogMessage(string playerName, PlayerActionsLogDTO actionLog)
        {
            MessageType = NetworkMessageType.ServerSentActionLog;
            ActionLog = actionLog;
            PlayerName = playerName;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(PlayerName);
            message = ActionLog.ToMessage(message);
            return message;
        }

        public new static ServerSentActionLogMessage FromMessageArguments(Message message, ref uint offset)
        {
            var playerName = message.GetString(offset++);
            var actionLog = PlayerActionsLogDTO.FromMessageArguments(message, ref offset);
            var dto = new ServerSentActionLogMessage(playerName, actionLog);
            return dto;
        }
    }
}
