using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientShare.PeristenceMessages
{
    public abstract class ClientPersistenceMessage<T> : PersitenceMessage<T>
    {
        public string Id { get; protected set; }

        protected ClientPersistenceMessage()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
