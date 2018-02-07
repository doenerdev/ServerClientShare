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

namespace ServerClientShare.DTO
{
    [Serializable]
    public class HexCellDTO : DTO<HexCellDTO>
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
            return dto;
        }
    }
}
