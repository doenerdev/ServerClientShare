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
    public class TowerService
    {
        private RandomGenerator _rndGenerator;

        public TowerService(ServerClientShare.Helper.RandomGenerator rndGenerator)
        {
            _rndGenerator = rndGenerator;
        }

        public TowerDTO GenerateNewTower()
        {
            TowerDTO dto = new TowerDTO();
            for (int i = 0; i < 3; i++)
            {
                var rand = _rndGenerator.RandomRange(0, _towerSegments[i].Count);
                dto.TowerSegments.Add(_towerSegments[i][rand]);
            }
            dto.CurrentTowerSegmentIndex = 0;

            return dto;
        }

        private Dictionary<int, List<TowerSegmentDTO>> _towerSegments = new Dictionary<int, List<TowerSegmentDTO>>()
        {
            //LEVEL 0
            {
                0, new List<TowerSegmentDTO>()
                {
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Stone),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant1,
                    },
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Stone),
                            new TowerResourceDTO(ResourceType.Stone),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant2,
                    },
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Sand),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant3,
                    },
                }
            },

            //LEVEL 1
            {
                1, new List<TowerSegmentDTO>()
                {
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Stone),
                            new TowerResourceDTO(ResourceType.Stone),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant1,
                    },
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Stone),
                            new TowerResourceDTO(ResourceType.Sand),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant2,
                    },
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Sand),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant3,
                    },
                }
            },

            //LEVEL 2
            {
                2, new List<TowerSegmentDTO>()
                {
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Wood),
                            new TowerResourceDTO(ResourceType.Sand),
                            new TowerResourceDTO(ResourceType.Sand),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant1,
                    },
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Stone),
                            new TowerResourceDTO(ResourceType.Stone),
                            new TowerResourceDTO(ResourceType.Stone),
                            new TowerResourceDTO(ResourceType.Stone),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant2,
                    },
                    new TowerSegmentDTO()
                    {
                        RequiredResources = new List<TowerResourceDTO>()
                        {
                            new TowerResourceDTO(ResourceType.Stone),
                            new TowerResourceDTO(ResourceType.Sand),
                            new TowerResourceDTO(ResourceType.Sand),
                        },
                        VisualStyle = TowerSegmentVisualStyle.Variant3,
                    },
                }
            },
        };
    }
}

