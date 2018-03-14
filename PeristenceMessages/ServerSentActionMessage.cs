using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentActionMessage : ServerPersistanceMessage<ServerSentActionMessage>
    {
        public string ActionName { get; set; }
        public string PlayerName { get; set; }
        public string ActionJson { get; set; }

        public ServerSentActionMessage(string actionName, string playerName, string actionJson)
        {
            MessageType = NetworkMessageType.ServerSentGameAction;
            ActionName = actionName;
            PlayerName = playerName;
            ActionJson = actionJson;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(ActionName);
            message.Add(PlayerName);
            message.Add(ActionJson);
            return message;
        }

        public new static ServerSentActionMessage FromMessageArguments(Message message, ref uint offset)
        {
            var actionName = message.GetString(offset++);
            var playerName = message.GetString(offset++);
            var actionJson = message.GetString(offset++);
            var dto = new ServerSentActionMessage(actionName, playerName, actionJson);
            return dto;
        }
    }
}
