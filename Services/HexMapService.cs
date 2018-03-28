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
using UnityEngine;

namespace ServerClientShare.Services
{
    public class HexMapService
    {
        private HexCellService _hexCellService;
        private HexMapDTO _currentHexMapDto;
        private List<PlayerDTO> _players;
        private Dictionary<int, List<int>> _playerZoneIndexes;
        private int _playerZoneRadius = 2;
        private List<int> _resourceZoneIndexes;

        public HexMapDTO CurrentHexMapDto => _currentHexMapDto ?? (_currentHexMapDto = GenerateNewHexMap(HexMapSize.L));

        public HexMapService(HexCellService hexCellService, List<PlayerDTO> players)
        {
            _hexCellService = hexCellService;
            _players = players;
            _playerZoneIndexes = new Dictionary<int, List<int>>();
            _resourceZoneIndexes = new List<int>();
        }

        public HexMapService(DatabaseObject dbObject, HexCellService hexCellService, List<PlayerDTO> players) : this(hexCellService, players)
        {
            _currentHexMapDto = HexMapDTO.FromDBObject(dbObject.GetObject("Marketplace"));
        }

        public HexMapService(HexMapDTO mapDto, HexCellService hexCellService, List<PlayerDTO> players) : this(hexCellService, players)
        {
            _currentHexMapDto = mapDto;
        }

        private void InitializeHexMapZones(HexMapDTO dto)
        {
            foreach (var player in _players)
            {
                _playerZoneIndexes.Add(player.PlayerIndex, new List<int>());
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

        private HexMapDTO GenerateNewHexMap(HexMapSize size)
        {
            Console.WriteLine("Generate New Hex Map");
            var dto = new HexMapDTO();
            var cells = new List<HexCellDTO>();

            InitializeHexMapZones(dto);

            for (int z = 0, i = 0; z < dto.Height; z++)
            {
                int width = z % 2 == 0
                    ? dto.Width
                    : dto.Width + 1;
                for (int x = 0; x < width; x++)
                {
                    for (int p = 0; p < _players.Count; p++) {
                        if (_playerZoneIndexes[_players[p].PlayerIndex].Contains(i))
                        {
                            cells.Add(_hexCellService.CreateHexCell(
                                x: x, 
                                z: z, 
                                i: i,
                                cellType:_players[p].CellType,
                                hasResource: false)
                            );
                            break;
                        }

                        if (p == _players.Count - 1)
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

            return dto;
        }

        public void UpdateHexMap(HexMapDTO dto)
        {
            _currentHexMapDto = dto;
            Debug.LogError("Hex map Units:" + _currentHexMapDto.Cells.Count(c => c.Units.Count > 0));
        }
    }
}
