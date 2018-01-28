using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerGameCode;

namespace ServerClientShare.DTO
{
    public class MatchDTO
    {
        public List<PlayerDTO> Players { get; private set; }
        public string GameId { get; set; }
        public int CurrentPlayerIndex { get; set; }
        public PlayerDTO CurrentPlayerDto => Players.Count > CurrentPlayerIndex && CurrentPlayerIndex >= 0 ? Players[CurrentPlayerIndex] : null; 

        public MatchDTO()
        {
            Players = new List<PlayerDTO>();
            CurrentPlayerIndex = 0;
        }

        public void AddPlayer(Player player)
        {
            var playerDto = new PlayerDTO()
            {
                PlayerIndex = Players.Count(),
                PlayerName = player.ConnectUserId
            };
            AddPlayer(playerDto);
        }

        public void AddPlayer(PlayerDTO playerDto)
        {
            if (Players.Count(pd => pd.PlayerName == playerDto.PlayerName) <= 0)
            {
                Players.Add(playerDto);
            }
        }

        public void RemovePlayer(Player player)
        {
            Players.RemoveAll(pd => pd.PlayerName == player.ConnectUserId);
        }

        public void RemovePlayer(PlayerDTO playerDto)
        {
            Players.Remove(playerDto);
        }
    }
}
