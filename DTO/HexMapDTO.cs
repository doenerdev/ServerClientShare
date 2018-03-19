
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
using ServerClientShare.Interfaces;
using ServerClientShare.Services;

namespace ServerClientShare.DTO
{
    [Serializable]
    public class HexMapDTO : DatabaseDTO<HexMapDTO>
    {
        public int Width { get; set; }
        public int Height {get; set;}
        public List<HexCellDTO> Cells { get; set; }

        public HexMapDTO()
        {
            Cells = new List<HexCellDTO>();
            Width = 5;
            Height = 5;
        }

        public override Message ToMessage(Message message)
        {
            message.Add(Width);
            message.Add(Height);
            message.Add(Cells.Count);

            foreach (var cell in Cells)
            {
                message = cell.ToMessage(message);
            }

            return message;
        }

        public new static HexMapDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexMapDTO dto = new HexMapDTO();
            dto.Width = message.GetInt(offset++);
            dto.Height = message.GetInt(offset++);

            var cellsCount = message.GetInt(offset++);
            for (int i = 0; i < cellsCount; i++)
            {
                dto.Cells.Add(HexCellDTO.FromMessageArguments(message, ref offset));
            }
            
            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("Width", Width);
            dbObject.Set("Height", Height);

            DatabaseArray cellsDB = new DatabaseArray();
            if (Cells != null)
            {
                foreach (var cell in Cells)
                {
                    cellsDB.Add(cell.ToDBObject());
                }
            }
            dbObject.Set("Cells", cellsDB);

            return dbObject;
        }

        public new static HexMapDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            HexMapDTO dto = new HexMapDTO();
            dto.Width = dbObject.GetInt("Width");
            dto.Height = dbObject.GetInt("Height");

            var cellsDB = dbObject.GetArray("Cells");
            for (int i = 0; i < cellsDB.Count; i++)
            {
                dto.Cells.Add(HexCellDTO.FromDBObject((DatabaseObject) cellsDB[i]));
            }

            return dto;
        }
    }
}
