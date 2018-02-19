
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Enums;
using ServerClientShare.Helper;

namespace ServerClientShare.DTO
{
    [Serializable]
    public class HexMapDTO : DTO<HexMapDTO>
    {
        public int Width { get; set; }
        public int Height {get; set;}
        public List<HexCellDTO> Cells { get; set; }

        public HexMapDTO(HexMapSize size)
        {
            Cells = new List<HexCellDTO>();
            switch (size)
            {
                default:
                    Width = 5;
                    Height = 5;
                    break;
            }
        }

        public override Message ToMessage(Message message)
        {
            message.Add(Width);
            message.Add(Height);

            foreach (var cell in Cells)
            {
                message = cell.ToMessage(message);
            }

            return message;
        }

        public new static HexMapDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexMapDTO dto = new HexMapDTO(HexMapSize.L);
            dto.Width = message.GetInt(offset++);
            dto.Height = message.GetInt(offset++);

            while (offset < message.Count)
            {
                dto.Cells.Add(HexCellDTO.FromMessageArguments(message, ref offset));
            }
            
            return dto;
        }
    }
}
