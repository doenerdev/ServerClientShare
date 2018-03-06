using System;
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
    public class HexCellDTO : DatabaseDTO<HexCellDTO>
    {
        public HexCellType HexCellType { get; set; }
        public TowerResourceDTO Resource { get; set; }
        public List<HexUnitDTO> Units { get; set; }

        public HexCellDTO()
        {
            Units = new List<HexUnitDTO>();
        }

        public HexCellDTO(TowerResourceDTO resource, List<HexUnitDTO> units)
        {
            Resource = resource;
            Units = units;
        }

        public override Message ToMessage(Message message)
        {
            message.Add((int)HexCellType);
            message.Add(Resource != null ? (int)Resource.Type : (int)ResourceType.None);
            message.Add(Units.Count);

            return message;
        }

        public new static HexCellDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexCellDTO dto = new HexCellDTO()
            {
                HexCellType = (HexCellType)message.GetInt(offset++),
                Resource = (ResourceType)message.GetInt(offset++) != ResourceType.None
                    ? new TowerResourceDTO((ResourceType)message.GetInt(offset-1)) : null
            };

            var unitCount = message.GetInt(offset++);
            for (int i = 0; i < unitCount; i++)
            {
                dto.Units.Add(HexUnitDTO.FromMessageArguments(message, ref offset));
            }

            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("HexCellType", (int) HexCellType);
            dbObject.Set("Resource", Resource != null ? Resource.ToDBObject() : new DatabaseObject());

            DatabaseArray unitsDB = new DatabaseArray();
            if (Units != null)
            {
                foreach (var unit in Units)
                {
                    unitsDB.Add(unit.ToDBObject());
                }
            }
            dbObject.Set("Units", unitsDB);

            return dbObject;
        }

        public new static HexCellDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            HexCellDTO dto = new HexCellDTO();
            dto.HexCellType = (HexCellType) dbObject.GetInt("HexCellType");
            dto.Resource = TowerResourceDTO.FromDBObject((DatabaseObject) dbObject.GetObject("Resource"));

            var unitsDB = dbObject.GetArray("Units");
            for (int i = 0; i < unitsDB.Count; i++)
            {
                dto.Units.Add(HexUnitDTO.FromDBObject((DatabaseObject)unitsDB[i]));
            }

            return dto;
        }
    }
}
