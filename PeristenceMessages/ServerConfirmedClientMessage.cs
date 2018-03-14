using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;

namespace ServerClientShare.PeristenceMessages
{
    public class ServerConfirmedClientMessage : ServerPersistanceMessage<ServerConfirmedClientMessage>
    {
        public string ApprovedMessageId { get; set; }

        public ServerConfirmedClientMessage(string approvedMessageId)
        {
            ApprovedMessageId = approvedMessageId;
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(ApprovedMessageId);
            return message;
        }

        public new static ServerConfirmedClientMessage FromMessageArguments(Message message, ref uint offset)
        {
            var approvedMessageId = message.GetString(offset++);
            var dto = new ServerConfirmedClientMessage(approvedMessageId) { };
            return dto;
        }
    }
}
