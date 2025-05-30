﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using ServerGameCode;
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.DTO;
using ServerClientShare.Models;

namespace ServerClientShare.DTO
{
    [Serializable]
    public class GameSessionsPersistenceDataDTO : DatabaseDTO<GameSessionsPersistenceDataDTO>
    {
        public string GameId { get; set; }
        public int CreatedTimestamp { get; set; }
        public int LastActivityTimestamp { get; set; }
        public string WinnerName { get; set; }
        public List<string> PlayerIds { get; set; }
        public List<PlayerMetaDataDTO> Players { get; set; }
        public PlayerActionsLog ActionLog { get; set; }
        public List<GameSessionTurnDataDTO> InitialTurns { get; set; }
        public List<GameSessionTurnDataDTO> Turns { get; set; }

        public GameSessionsPersistenceDataDTO()
        {
            PlayerIds = new List<string>();
            Players = new List<PlayerMetaDataDTO>();
            InitialTurns = new List<GameSessionTurnDataDTO>();
            Turns = new List<GameSessionTurnDataDTO>();
        }

        public override Message ToMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("GameId", GameId);
            dbObject.Set("CreatedTimestamp", CreatedTimestamp);
            dbObject.Set("LastActivityTimestamp", LastActivityTimestamp);

            DatabaseArray playerIdsDB = new DatabaseArray();
            if (PlayerIds != null)
            {
                foreach (var playerId in PlayerIds)
                {
                    playerIdsDB.Add(playerId);
                }
            }
            dbObject.Set("PlayerIds", playerIdsDB);

            dbObject.Set("PlayerActionLog", ActionLog.ToDBObject());

            DatabaseArray initialTurnsDB = new DatabaseArray();
            if (Turns != null)
            {
                foreach (var initialTurn in InitialTurns)
                {
                    initialTurnsDB.Add(initialTurn.ToDBObject());
                }
            }
            dbObject.Set("InitialTurns", initialTurnsDB);

            DatabaseArray turnsDB = new DatabaseArray();
            if (Turns != null)
            {
                foreach (var turn in Turns)
                {
                    turnsDB.Add(turn.ToDBObject());
                }
            }
            dbObject.Set("Turns", turnsDB);

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

#if !(UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0)
        public new static GameSessionsPersistenceDataDTO FromDBObject(DatabaseObject dbObject, ServerCode server)
        {
            Console.WriteLine("Count:" + dbObject.Count);
            if(dbObject.Count == 0) return null;

            GameSessionsPersistenceDataDTO dto = new GameSessionsPersistenceDataDTO();
            dto.GameId = dbObject.GetString("GameId");
            dto.CreatedTimestamp = dbObject.GetInt("CreatedTimestamp");
            dto.LastActivityTimestamp = dbObject.GetInt("LastActivityTimestamp");

            var playerIdsDB = dbObject.GetArray("PlayerIds");
            for (int i = 0; i < playerIdsDB.Count; i++)
            {
                dto.PlayerIds.Add(playerIdsDB.GetString(i));
            }

            dto.ActionLog = PlayerActionsLog.FromDBObject(dbObject.GetObject("PlayerActionLog"), server);

            var initialTurnsDB = dbObject.GetArray("InitialTurns");
            for (int i = 0; i < initialTurnsDB.Count; i++)
            {
                dto.InitialTurns.Add(GameSessionTurnDataDTO.FromDBObject((DatabaseObject)initialTurnsDB.GetObject(i)));
            }

            var turnsDB = dbObject.GetArray("Turns");
            for (int i = 0; i < turnsDB.Count; i++)
            {
                dto.Turns.Add(GameSessionTurnDataDTO.FromDBObject((DatabaseObject)turnsDB.GetObject(i)));
            }

            var playersDB = dbObject.GetArray("Players");
            for (int i = 0; i < playersDB.Count; i++)
            {
                dto.Players.Add(PlayerMetaDataDTO.FromDBObject((DatabaseObject)playersDB.GetObject(i)));
            }

            return dto;
        }
    }
#else
        public new static GameSessionsPersistenceDataDTO FromDBObject(DatabaseObject dbObject)
        {
      Console.WriteLine("Count:" + dbObject.Count);
            if (dbObject.Count == 0) return null;

            GameSessionsPersistenceDataDTO dto = new GameSessionsPersistenceDataDTO();
            dto.GameId = dbObject.GetString("GameId");
            dto.CreatedTimestamp = dbObject.GetInt("CreatedTimestamp");
            dto.LastActivityTimestamp = dbObject.GetInt("LastActivityTimestamp");

            var playerIdsDB = dbObject.GetArray("PlayerIds");
            for (int i = 0; i < playerIdsDB.Count; i++)
            {
                dto.PlayerIds.Add(playerIdsDB[i].ToString());
            }

            dto.ActionLog = PlayerActionsLog.FromDBObject(dbObject.GetObject("PlayerActionLog"));

            var initialTurnsDB = dbObject.GetArray("InitialTurns");
            foreach (object initialTurn in initialTurnsDB)
            {
                dto.InitialTurns.Add(GameSessionTurnDataDTO.FromDBObject((DatabaseObject)initialTurn));
            }

            var turnsDB = dbObject.GetArray("Turns");
            foreach (object turn in turnsDB)
            {
                dto.Turns.Add(GameSessionTurnDataDTO.FromDBObject((DatabaseObject)turn));
            }

            var playersDB = dbObject.GetArray("Players");
            for (int i = 0; i < playersDB.Count; i++)
            {
                dto.Players.Add(PlayerMetaDataDTO.FromDBObject((DatabaseObject)playersDB.GetObject(i)));
            }

            return dto;
        }
    }
#endif
}
