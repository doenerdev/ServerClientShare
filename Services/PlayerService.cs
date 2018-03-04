using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.Services
{
    public class PlayerService
    {
        private readonly ResourceService _resourceService;
        private readonly LeaderService _leaderService;

        public PlayerService(ResourceService resourceService, LeaderService leaderService)
        {
            _resourceService = resourceService;
            _leaderService = leaderService;
        }

        public PlayerDTO GenerateInitialPlayer(string playerName, int index, LeaderType leaderType, ControlMode type = ControlMode.Remote)
        {
            var playerDto = new PlayerDTO()
            {
                PlayerIndex = index,
                PlayerName = playerName,
                ControlMode = type,
                CurrentTowerSegment = _resourceService.GenerateNewTowerSegment(),
                Leader = _leaderService.CreateLeader(leaderType)
            };

            return playerDto;
        }
    }
}
