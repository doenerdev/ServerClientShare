
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class HexUnitDTO : DTO
    {
        public int PlayerId { get; set; }
        public HexCoordinatesDTO Coordinates { get; set; }
        public HexUnitType Type { get; set; }
        public int Stamina { get; set; }
    }
}
