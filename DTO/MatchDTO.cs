using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class MatchDTO : DTO<MatchDTO>
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

        public void AddPlayer(string playerName, PlayerType type = PlayerType.Remote)
        {
            if (Players.Count(pd => pd.PlayerName == playerName) > 0) return;

            var playerDto = new PlayerDTO()
            {
                PlayerIndex = Players.Count(),
                PlayerName = playerName,
                PlayerType =  type,
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

        public override Message ToMessage(Message message)
        {
            message.Add(GameId);
            message.Add(CurrentPlayerIndex);
            message.Add(Players.Count);

            foreach (var player in Players)
            {
                message = player.ToMessage(message);
            }

            return message;
        }

        public new static MatchDTO FromMessageArguments(Message message, ref uint offset)
        {
            MatchDTO dto = new MatchDTO();
            dto.GameId = message.GetString(offset++);
            dto.CurrentPlayerIndex = message.GetInt(offset++);

            for (int i = 0; i < message.GetInt(offset++); i++)
            {
                dto.Players.Add(PlayerDTO.FromMessageArguments(message, ref offset));
            }

            return dto;
        }
    }
}
