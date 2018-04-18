using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientShare.DTO;
using ServerClientShare.Enums;
using ServerClientShare.Helper;

namespace ServerClientShare.Services
{
    public class HexCellService
    {
        private ServerClientShare.Helper.Die _die;
        private ServerClientShare.Helper.RandomGenerator _rndGenerator;

        public HexCellService(ServerClientShare.Helper.Die die, ServerClientShare.Helper.RandomGenerator rndGenerator)
        {
            _die = die;
            _rndGenerator = rndGenerator;
        }

        public HexCellDTO CreateHexCell(int x, int z, int i, HexCellType cellType, bool hasResource = false)
        {
            TowerResourceDTO resource = null;
            if (hasResource)
            {
                switch (cellType)
                {
                    case HexCellType.Forest:
                        resource = new TowerResourceDTO(ResourceType.Wood);
                        break;
                    case HexCellType.Desert:
                        resource = new TowerResourceDTO(ResourceType.Sand);
                        break;
                    case HexCellType.Mountains:
                        resource = new TowerResourceDTO(ResourceType.Stone);
                        break;
                }
            }

            return new HexCellDTO()
            {
                HexCellType = cellType,
                Resource = resource,
            };
        }

        public HexCellDTO CreateHexCell(int x, int z, int i, bool hasResource = false)
        {
            //randomly select tile type
            var rnd = _rndGenerator.RandomRange(0f, 1f);
            HexCellType type = HexCellType.Desert;
            if (rnd >= 0.8f)
            {
                type = HexCellType.Mountains;
            }
            else if (rnd >= 0.58f)
            {
                type = HexCellType.Forest;
            }

            return CreateHexCell(x, z, i, type, hasResource);
        }
    }
}
