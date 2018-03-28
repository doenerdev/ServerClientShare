using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using ServerClientShare.Interfaces;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif

namespace ServerClientShare.DTO
{
    [Serializable]
    public class HexCoordinatesDTO : DTO<HexCoordinatesDTO>, IDBSerializable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override Message ToMessage(Message message)
        {
            message.Add(X);
            message.Add(Y);
            message.Add(Z);
            return message;
        }

        public new static HexCoordinatesDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexCoordinatesDTO dto = new HexCoordinatesDTO()
            {
                X = message.GetInt(offset++),
                Y = message.GetInt(offset++),
                Z = message.GetInt(offset++),
            };
            return dto;
        }

        public DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("X", X);
            dbObject.Set("Y", Y);
            dbObject.Set("Z", Z);
            return dbObject;
        }

        public new static HexCoordinatesDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            HexCoordinatesDTO dto = new HexCoordinatesDTO();
            dto.X = dbObject.GetInt("X");
            dto.Y = dbObject.GetInt("Y");
            dto.Z = dbObject.GetInt("Z");

            return dto;
        }
    }
}
