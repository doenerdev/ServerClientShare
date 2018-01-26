using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientShare.DTO
{
    public class HexMapDTO
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
    }
}
