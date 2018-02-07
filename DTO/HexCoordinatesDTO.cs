using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;

namespace ServerClientShare.DTO
{
    public class HexCoordinatesDTO : DTO<HexCoordinatesDTO>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override Message ToMessage(Message message)
        {
            message.Add(X);
            message.Add(Y);
            message.Add(Z);
            return message;
        }

        public new static HexCoordinatesDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexCoordinatesDTO dto = new HexCoordinatesDTO()
            {
                X = message.GetInt(offset++),
                Y = message.GetInt(offset++),
                Z = message.GetInt(offset++),
            };
            return dto;
        }
    }
}
