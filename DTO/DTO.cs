using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIO.GameLibrary;
#else
using PlayerIO.GameLibrary;
#endif

using ServerClientShare.Enums;
//using Newtonsoft.Json;
using ServerClientShare.MiniJson;
using ServerClientShare.Interfaces;

namespace ServerClientShare.DTO
{
    [Serializable]
    public abstract class DTO<T> : IMessageSerializable<T>
    {
        public abstract object[] ToMessageArguments(ref object[] args);

        public static T FromMessage(Message message)
        {
            return default(T);
        }
    }
}
