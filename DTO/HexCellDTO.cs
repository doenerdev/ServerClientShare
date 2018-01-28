using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class HexCellDTO : DTO
    {
        public HexCellType HexCellType { get; set; }
        public HexCoordinatesDTO Coordinates { get; set; }
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
    }
}
