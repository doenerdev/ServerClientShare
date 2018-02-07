
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class HexUnitDTO : DTO<HexUnitDTO>
    {
        public int PlayerId { get; set; }
        public HexCoordinatesDTO Coordinates { get; set; }
        public HexUnitType Type { get; set; }
        public int Stamina { get; set; }
        public string Id { get; set; }

        public override Message ToMessage(Message message)
        {
            message.Add(Id);
            message.Add(PlayerId);
            message.Add((int) Type);
            message.Add(Stamina);
            message = Coordinates.ToMessage(message);
            return message;
        }

        public new static HexUnitDTO FromMessageArguments(Message message, ref uint offset)
        {
            HexUnitDTO dto = new HexUnitDTO();
            dto.Id = message.GetString(offset++);
            dto.PlayerId = message.GetInt(offset++);
            dto.Type = (HexUnitType) message.GetInt(offset++);
            dto.Stamina = message.GetInt(offset++);
            dto.Coordinates = HexCoordinatesDTO.FromMessageArguments(message, offset);
            return dto;
        }
    }
}
