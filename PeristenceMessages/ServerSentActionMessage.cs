using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerSentActionMessage : ClientSentActionMessage
    {
        public ServerSentActionMessage(string actionName, string playerName, string actionJson) : base(actionName, playerName, actionJson)
        {
        }
    }
}
