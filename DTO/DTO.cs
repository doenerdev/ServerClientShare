using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using PlayerIO.GameLibrary;
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
