﻿using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.Enums;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif

namespace ServerClientShare.PeristenceMessages
{
    public class ClientConfirmedServerMessage : ServerPersistanceMessage<ServerConfirmedClientMessage>
    {
        public string ApprovedMessageId { get; set; }

        public ClientConfirmedServerMessage(string approvedMessageId)
        {
            ApprovedMessageId = approvedMessageId;
            MessageType = NetworkMessageType.ClientSentConfirmedServerMessage; 
        }

        public override Message ToMessage()
        {
            var message = Message.Create(MessageType.ToString("G"));
            message.Add(Id);
            message.Add(ApprovedMessageId);
            return message;
        }

        public new static ClientConfirmedServerMessage FromMessageArguments(Message message, ref uint offset)
        {
            var id = message.GetString(offset++);
            var approvedMessageId = message.GetString(offset++);
            var dto = new ClientConfirmedServerMessage(approvedMessageId) { Id = id };
            return dto;
        }
    }
}
