using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;

namespace ServerClientShare.PeristenceMessages
{
    public class ClientSentEndedGameMessage : ClientPersistenceMessage<ClientSentEndedGameMessage>
    {
        public override Message ToMessage()
        {
            throw new NotImplementedException();
        }
    }
}
