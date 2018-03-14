using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.PeristenceMessages
{
    public class DatalessClientMessage : ClientPersistenceMessage<DatalessClientMessage>
    {
        public DatalessClientMessage(NetworkMessageType type)
        {
            MessageType = type;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            return message;
        }

        public new static DatalessClientMessage FromMessageArguments(Message message, ref uint offset)
        {
            var type = (NetworkMessageType) message.GetInt(offset++);
            return new DatalessClientMessage(type);
        }
    }
}
