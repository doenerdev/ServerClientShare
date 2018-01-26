using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class TowerResourceDTO
    {
        public ResourceType Type { get; set; }

        public TowerResourceDTO(ResourceType type)
        {
            Type = type;
        }
    }
}
