using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerClientShare.DTO
{
    public class MatchDTO : DTO
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

        public void AddPlayer(string playerName)
        {
            if (Players.Count(pd => pd.PlayerName == playerName) > 0) return;

            var playerDto = new PlayerDTO()
            {
                PlayerIndex = Players.Count(),
                PlayerName = playerName
            };
            Players.Add(playerDto);
        }

        public void AddPlayer(PlayerDTO playerDto)
        {
            if (Players.Count(pd => pd.PlayerName == playerDto.PlayerName) > 0) return;

            Players.Add(playerDto);
        }

        public void RemovePlayer(string playerName)
        {
            Players.RemoveAll(pd => pd.PlayerName == playerName);
        }

        public void RemovePlayer(PlayerDTO playerDto)
        {
            Players.Remove(playerDto);
        }
    }
}
