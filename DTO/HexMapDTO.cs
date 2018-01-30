
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIO.GameLibrary;
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
        public int Width = 10;
        public int Height = 10;
        public List<HexCellDTO> Cells { get; set; }

        public HexMapDTO(HexMapSize size)
        {
            Cells = new List<HexCellDTO>();
            switch (size)
            {
                default:
                    Width = 10;
                    Height = 10;
                    break;
            }
        }

        public override object[] ToMessageArguments(ref object[] args)
        {
            List<object> argsList = new List<object>();
            argsList.Add(Width);
            argsList.Add(Height);

            foreach (var cell in Cells)
            {
                argsList.Add(cell.ToMessageArguments(ref args));
            }

            return args.Concat(argsList.ToArray()).ToArray();
        }

        public Message ToMessage(Message message)
        {
            message.Add(Width);
            message.Add(Height);

            foreach (var cell in Cells)
            {
                message = cell.ToMessage(message);
            }

            return message;
        }

        public static HexMapDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexMapDTO dto = new HexMapDTO(HexMapSize.L)
            {
                Width = message.GetInt(offset++),
                Height = message.GetInt(offset++),
            };

            while (offset < message.Count)
            {
                dto.Cells.Add(HexCellDTO.FromMessageArguments(message, ref offset));
            }
            

            return dto;
        }
    }
}
