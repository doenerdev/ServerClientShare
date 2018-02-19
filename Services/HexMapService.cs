using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientShare.DTO;

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

        private HexMapDTO GenerateNewHexMap(HexMapSize size)
        {
            Console.WriteLine("Generate New Hex Map");
            var dto = new HexMapDTO(size);
            var cells = new List<HexCellDTO>();

            for (int z = 0, i = 0; z < dto.Height; z++)
            {
                for (int x = 0; x < dto.Width; x++)
                {
                    cells.Add(_hexCellService.CreateHexCell(x, z, i++));
                }
            }
            dto.Cells = cells;

            return dto;
        } 
    }
}
