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
using ServerGameCode;

namespace ServerClientShare.Services
{
    public class HexMapService
    {
        private HexCellService _hexCellService;
        private HexMapDTO _currentHexMapDto;
        private HexMapSize _mapSize;

        public HexMapDTO CurrentHexMapDto => _currentHexMapDto ?? (_currentHexMapDto = GenerateNewHexMap(HexMapSize.L));

        public HexMapService(HexCellService hexCellService, HexMapSize mapSize)
        {
            _hexCellService = hexCellService;
            _mapSize = mapSize;
        }

        public HexMapService(DatabaseObject dbObject, HexCellService hexCellService, HexMapSize mapSize) : this(hexCellService, mapSize)
        {
            _currentHexMapDto = HexMapDTO.FromDBObject(dbObject.GetObject("Marketplace"));
        }

        public HexMapService(GameSessionsPersistenceDataDTO sessionData, HexCellService hexCellService, HexMapSize mapSize) : this(hexCellService, mapSize)
        {
            _currentHexMapDto = sessionData.Turns.Last().HexMap;
        }

        private HexMapDTO GenerateNewHexMap(HexMapSize size)
        {
            Console.WriteLine("Generate New Hex Map");
            var dto = new HexMapDTO(size);
            var cells = new List<HexCellDTO>();

            for (int z = 0, i = 0; z < dto.Height; z++)
            {
                int width = z % 2 == 0
                    ? dto.Width
                    : dto.Width + 1;
                for (int x = 0; x < width; x++)
                {
                    cells.Add(_hexCellService.CreateHexCell(x, z, i++));
                }
            }
            dto.Cells = cells;

            return dto;
        }

        public void UpdateHexMap(HexMapDTO dto)
        {
            _currentHexMapDto = dto;
        }
    }
}
