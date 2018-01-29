using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientShare.DTO
{
    public class HexCoordinatesDTO : DTO<HexCoordinatesDTO>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override object[] ToMessageArguments(ref object[] args)
        {
            return null;
        }
    }
}
