using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentActionMessage : ClientSentActionMessage
    {
        public ServerSentActionMessage(string actionName, string playerName, string actionJson) : base(actionName, playerName, actionJson)
        {
            MessageType = NetworkMessageType.ServerSentGameAction;
        }
    }
}
