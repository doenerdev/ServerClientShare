
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Enums;
using ServerClientShare.Interfaces;

namespace ServerClientShare.DTO
{
    [Serializable]
    public class HexUnitDTO : DatabaseDTO<HexUnitDTO>
    {
        public int PlayerId { get; set; }
        public HexCoordinatesDTO Coordinates { get; set; }
        public HexUnitType Type { get; set; }
        public int Stamina { get; set; }
        public int MaxStamina { get; set; }
        public string Id { get; set; }

        public override Message ToMessage(Message message)
        {
            message.Add(PlayerId);
            message.Add((int) Type);
            message.Add(Stamina);
            message.Add(MaxStamina);
            message = Coordinates.ToMessage(message);
            message.Add(Id);
            return message;
        }

        public new static HexUnitDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexUnitDTO dto = new HexUnitDTO();
            dto.PlayerId = message.GetInt(offset++);
            dto.Type = (HexUnitType) message.GetInt(offset++);
            dto.Stamina = message.GetInt(offset++);
            dto.MaxStamina = message.GetInt(offset++);
            dto.Coordinates = HexCoordinatesDTO.FromMessageArguments(message, ref offset);
            dto.Id = message.GetString(offset++);
            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("PlayerId", PlayerId);
            dbObject.Set("Coordinates", Coordinates.ToDBObject());
            dbObject.Set("Type", (int) Type);
            dbObject.Set("Stamina", Stamina);
            dbObject.Set("MaxStamina", MaxStamina);
            dbObject.Set("Id", Id);

            return dbObject;
        }

        public new static HexUnitDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            HexUnitDTO dto = new HexUnitDTO();
            dto.PlayerId = dbObject.GetInt("PlayerId");
            dto.Coordinates = HexCoordinatesDTO.FromDBObject(dbObject.GetObject("Coordinates"));
            dto.Type = (HexUnitType) dbObject.GetInt("Type");
            dto.Stamina = dbObject.GetInt("Stamina");
            dto.MaxStamina = dbObject.GetInt("MaxStamina");
            dto.Id = dbObject.GetString("Id");

            return dto;
        }
    }
}
