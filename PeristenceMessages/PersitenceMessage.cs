using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Enums;
using ServerClientShare.Interfaces;

namespace ServerClientShare.PeristenceMessages
{
    public abstract class PersitenceMessage<T> : IPersistenceMessage
    {
        public NetworkMessageType MessageType { get; protected set; }

        public abstract Message ToMessage();

        public static T FromMessageArguments(Message message, ref uint offset)
        {
            return default(T);
        }
    }
}
