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
using ServerClientShare.Enums;


namespace ServerClientShare.DTO
{
    [Serializable]
    public class GameSessionMetaDataDTO : DatabaseDTO<GameSessionMetaDataDTO>
    {
        public string GameId { get; set; }
        public GameStartedState GameStartedState { get; set; }
        public string CurrentPlayerName { get; set; }
        public int TurnNumber { get; set; }
        public int RequiredRoomSize { get; set; }
        public List<PlayerMetaDataDTO> Players { get; set; }

        public GameSessionMetaDataDTO()
        {
            Players = new List<PlayerMetaDataDTO>();
        }

        public override Message ToMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public override DatabaseObject ToDBObject()
        {
            var dbObject = new DatabaseObject();
            dbObject.Set("GameId", GameId);
            dbObject.Set("GameStartedState", (int) GameStartedState);
            dbObject.Set("CurrentPlayerName", CurrentPlayerName);
            dbObject.Set("TurnNumber", TurnNumber);
            dbObject.Set("RequiredRoomSize", RequiredRoomSize);

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

        public new static GameSessionMetaDataDTO FromDBObject(DatabaseObject dbObject)
        {
            var dto = new GameSessionMetaDataDTO();
            dto.GameId = dbObject.GetString("GameId");
            dto.GameStartedState = (GameStartedState) dbObject.GetInt("GameStartedState");
            dto.CurrentPlayerName = dbObject.GetString("CurrentPlayerName");
            dto.TurnNumber = dbObject.GetInt("TurnNumber");
            dto.RequiredRoomSize = dbObject.GetInt("RequiredRoomSize");

            var playersDB = dbObject.GetArray("Turns");
            foreach (object player in playersDB)
            {
                dto.Players.Add(PlayerMetaDataDTO.FromDBObject((DatabaseObject)player));
            }

            return dto;
        }
    }
}
