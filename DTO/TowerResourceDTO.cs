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

namespace ServerClientShare.DTO
{
    public class TowerResourceDTO : DatabaseDTO<TowerResourceDTO>
    {
        public ResourceType Type { get; set; }

        public TowerResourceDTO(ResourceType type)
        {
            Type = type;
        }

        public override Message ToMessage(Message message)
        {
            message.Add((int) Type);
            return message;
        }

        public new static TowerResourceDTO FromMessageArguments(Message message, ref uint offset)
        {
            TowerResourceDTO dto = new TowerResourceDTO((ResourceType) message.GetInt(offset++));
            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("Type", (int) Type);
            return dbObject;
        }

        public new static TowerResourceDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            var type = (ResourceType) dbObject.GetInt("Type");
            TowerResourceDTO dto = new TowerResourceDTO(type);
            
            return dto;
        }
    }
}
