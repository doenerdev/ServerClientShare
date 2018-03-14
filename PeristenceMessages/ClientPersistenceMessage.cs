using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;
using ServerClientShare.Interfaces;

namespace ServerClientShare.PeristenceMessages
{
    public abstract class ClientPersistenceMessage<T> : IClientPersistanceMessage
    {
        public string Id { get; protected set; }
        public NetworkMessageType MessageType { get; protected set; }
        
        protected ClientPersistenceMessage()
        {
            Id = Guid.NewGuid().ToString();
        }

        public abstract Message ToMessage();

        public static T FromMessageArguments(Message message, ref uint offset)
        {
            return default(T);
        }
    }
}
