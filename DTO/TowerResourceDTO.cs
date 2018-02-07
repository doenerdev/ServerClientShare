using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class TowerResourceDTO : DTO<TowerResourceDTO>
    {
        public ResourceType Type { get; set; }

        public TowerResourceDTO(ResourceType type)
        {
            Type = type;
        }

        public override Message ToMessage(Message message)
        {
            message.Add((int) Type);
            return message;
        }

        public new static TowerResourceDTO FromMessageArguments(Message message, ref uint offset)
        {
            TowerResourceDTO dto = new TowerResourceDTO((ResourceType) message.GetInt(offset++));
            return dto;
        }
    }
}
