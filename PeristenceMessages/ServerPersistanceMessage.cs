using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;
using ServerClientShare.Interfaces;

namespace ServerClientShare.PeristenceMessages
{
    public abstract class ServerPersistanceMessage<T> : IServerPersistenceMessage
    {
        public NetworkMessageType MessageType { get; }

        public abstract Message ToMessage();

        public static T FromMessageArguments(Message message, ref uint offset)
        {
            return default(T);
        }
    }
}
