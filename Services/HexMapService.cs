using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.Services
{
    public class HexMapService
    {
        private HexCellService _hexCellService;
        private HexMapDTO _currentHexMapDto;
        private Dictionary<int, List<int>> _playerZoneIndexes;
        private int _playerCount;
        private int _playerZoneRadius = 2;
        private List<int> _resourceZoneIndexes;

        public HexMapDTO CurrentHexMapDto => _currentHexMapDto;

        public HexMapService(HexCellService hexCellService, int playerCount)
        {
            _playerCount = playerCount;
            _hexCellService = hexCellService;
            _playerZoneIndexes = new Dictionary<int, List<int>>();
            _resourceZoneIndexes = new List<int>();
        }

        public HexMapService(DatabaseObject dbObject, HexCellService hexCellService, int playerCount) : this(hexCellService, playerCount)
        {
            _currentHexMapDto = HexMapDTO.FromDBObject(dbObject.GetObject("HexMap"));
            Console.WriteLine("Initialized From DB Object");
            Console.WriteLine(_currentHexMapDto.Cells.Count);
        }

        public HexMapService(HexMapDTO mapDto, HexCellService hexCellService, int playerCount) : this(hexCellService, playerCount)
        {
            _currentHexMapDto = mapDto;
        }

        private void InitializeHexMapZones(HexMapDTO dto)
        {
            for(int i = 0; i < _playerCount; i++)
            {
                _playerZoneIndexes.Add(i, new List<int>());
            }

            for (int z = 0, i = 0; z < dto.Height; z++)
            {
                int width = z % 2 == 0
                    ? dto.Width
                    : dto.Width + 1;
                for (int x = 0; x < width; x++)
                {
                    if ((x < _playerZoneRadius && z != 2) 
                        || (x < _playerZoneRadius-1 && z == 2))
                    {
                        _playerZoneIndexes[0].Add(i);
                    }
                    else if ((x >= dto.Width - _playerZoneRadius && z % 2 == 0 && z != 2)
                        || (x > dto.Width - _playerZoneRadius && (z % 2 != 0 || z == 2)))
                    {
                        _playerZoneIndexes[1].Add(i);
                    }
                    else
                    {
                        _resourceZoneIndexes.Add(i);
                    }

                    i++;
                }
            }
        }

        public void GenerateNewHexMap(List<PlayerDTO> _players)
        {
            Console.WriteLine("Generate New Hex Map");
   
            var dto = new HexMapDTO();
            var cells = new List<HexCellDTO>();
            Console.WriteLine("Height:" + dto.Height + " Width:");
            Console.WriteLine("Players Count:" + _players.Count);
  
            Console.WriteLine("PC:" + _playerCount);
            InitializeHexMapZones(dto);

            for (int z = 0, i = 0; z < dto.Height; z++)
            {
                int width = z % 2 == 0
                    ? dto.Width
                    : dto.Width + 1;
                for (int x = 0; x < width; x++)
                {
                    for (int p = 0; p < _playerCount; p++) {
                        if (_playerZoneIndexes[p].Contains(i))
                        {
                            cells.Add(_hexCellService.CreateHexCell(
                                x: x, 
                                z: z, 
                                i: i,
                                cellType: _players[p].CellType,
                                hasResource: false)
                            );
                            break;
                        }

                        if (p == _playerCount - 1)
                        {
                            cells.Add(_hexCellService.CreateHexCell(
                                x: x,
                                z: z,
                                i: i,
                                hasResource: _resourceZoneIndexes.Contains(i))
                            );
                        }

                    }
                    i++;
                }
            }
            dto.Cells = cells;

            _currentHexMapDto = dto;
        }

        public void UpdateHexMap(HexMapDTO dto)
        {
            _currentHexMapDto = dto;
        }
    }
}
