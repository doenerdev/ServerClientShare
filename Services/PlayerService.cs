using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.Services
{
    public class PlayerService
    {
        private ResourceService _resourceService;

        public PlayerService(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public PlayerDTO GenerateInitialPlayer(string playerName, int index, ControlMode type = ControlMode.Remote)
        {
            var playerDto = new PlayerDTO()
            {
                PlayerIndex = index,
                PlayerName = playerName,
                ControlMode = type,
                CurrentTowerSegment = _resourceService.GenerateNewTowerSegment()
            };

            return playerDto;
        }
    }
}
