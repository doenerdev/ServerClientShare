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
        private readonly TowerService _towerService;

        public PlayerService(ResourceService resourceService, LeaderService leaderService, TowerService towerService)
        {
            _resourceService = resourceService;
            _leaderService = leaderService;
            _towerService = towerService;
        }

        public PlayerDTO GenerateInitialPlayer(string playerName, int index, LeaderType leaderType, ControlMode type = ControlMode.Remote)
        {
            var playerDto = new PlayerDTO()
            {
                PlayerIndex = index,
                PlayerName = playerName,
                ControlMode = type,
                Tower = _towerService.GenerateNewTower(),
                Leader = _leaderService.CreateLeader(leaderType)
            };
            playerDto.CurrentTowerSegment = playerDto.Tower.TowerSegments[playerDto.Tower.CurrentTowerSegmentIndex];

            return playerDto;
        }
    }
}
