using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    [Serializable]
    public class MatchDTO : DatabaseDTO<MatchDTO>
    {
        public List<PlayerDTO> Players { get; private set; }
        public string GameId { get; set; }
        public int CurrentPlayerIndex { get; set; }
        public GamePhase GamePhase { get; set; }
        public int TurnNumber { get; set; }
        public PlayerDTO CurrentPlayerDto => Players.Count > CurrentPlayerIndex && CurrentPlayerIndex >= 0 ? Players[CurrentPlayerIndex] : null; 

        public MatchDTO()
        {
            Players = new List<PlayerDTO>();
            CurrentPlayerIndex = 0;
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
            message.Add((int) GamePhase);
            message.Add(TurnNumber);
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
            dto.GamePhase = (GamePhase) message.GetInt(offset++);
            dto.TurnNumber = message.GetInt(offset++); 
            var qtyPlayers = message.GetInt(offset++);

            for (int i = 0; i < qtyPlayers; i++)
            {
                dto.Players.Add(PlayerDTO.FromMessageArguments(message, ref offset));
            }

            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("GameId", GameId);
            dbObject.Set("CurrentPlayerIndex", CurrentPlayerIndex);
            dbObject.Set("TurnNumber", TurnNumber);
            dbObject.Set("GamePhase", (int) GamePhase);

            DatabaseArray playersDB = new DatabaseArray();
            if (Players != null)
            {
                foreach (var player in Players)
                {
                    playersDB.Add(player.ToDBObject());
                }
            }
            dbObject.Set("Players", playersDB);

            return dbObject;
        }

        public new static MatchDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            MatchDTO dto = new MatchDTO();
            dto.GameId = dbObject.GetString("GameId");
            dto.CurrentPlayerIndex = dbObject.GetInt("CurrentPlayerIndex");
            dto.TurnNumber = dbObject.GetInt("TurnNumber");
            dto.GamePhase = (GamePhase) dbObject.GetInt("GamePhase");

            var playersDB = dbObject.GetArray("Players");
            for (int i = 0; i < playersDB.Count; i++)
            {
                dto.Players.Add(PlayerDTO.FromDBObject((DatabaseObject)playersDB[i]));
            }

            return dto;
        }
    }
}
