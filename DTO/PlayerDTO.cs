using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    [Serializable]
    public class PlayerDTO : DatabaseDTO<PlayerDTO>
    {
        public int PlayerIndex { get; set; }
        public string PlayerName { get; set; }
        public ControlMode ControlMode { get; set; }
        public int CurrentTurn { get; set; }
        public int CurrentActionLogIndex { get; set; }
        public int Score { get; set; }
        public int CardsDrawn { get; set; }
        public int CardsPlayed { get; set; }
        public TowerSegmentDTO CurrentTowerSegment { get; set; }
        public LeaderDTO Leader { get; set; }
        public List<TowerResourceDTO> Resources { get; set; }
        public DeckDTO Hand { get; set; }

        public HexCellType CellType
        {
            get
            {
                var cellType = HexCellType.Desert;
                switch (Leader.Type)
                {
                    case LeaderType.Dwarf:
                        cellType = HexCellType.Mountains;
                        break;
                    case LeaderType.Elf:
                        cellType = HexCellType.Forest;
                        break;
                }
                return cellType;
            }
        }

        public PlayerDTO()
        {
            Resources = new List<TowerResourceDTO>();
            Hand = new DeckDTO();
        }

        public override Message ToMessage(Message message)
        {
            message.Add(PlayerIndex);
            message.Add(PlayerName);
            message.Add((int) ControlMode);
            message.Add(CurrentTurn);
            message.Add(CurrentActionLogIndex);
            message.Add(Score);
            message.Add(CardsDrawn);
            message.Add(CardsPlayed);
            message = CurrentTowerSegment.ToMessage(message);
            message = Leader.ToMessage(message);

            message.Add(Resources.Count);
            foreach (var resource in Resources)
            {
                message = resource.ToMessage(message);
            }

            message = Hand.ToMessage(message);

            return message;
        }

        public new static PlayerDTO FromMessageArguments(Message message, ref uint offset)
        {
            PlayerDTO dto = new PlayerDTO();
            dto.PlayerIndex = message.GetInt(offset++);
            dto.PlayerName = message.GetString(offset++);
            dto.ControlMode = (ControlMode) message.GetInt(offset++);
            dto.CurrentTurn = message.GetInt(offset++);
            dto.CurrentActionLogIndex = message.GetInt(offset++);
            dto.Score = message.GetInt(offset++);
            dto.CardsDrawn = message.GetInt(offset++);
            dto.CardsPlayed = message.GetInt(offset++);
            dto.CurrentTowerSegment = TowerSegmentDTO.FromMessageArguments(message, ref offset);
            dto.Leader = LeaderDTO.FromMessageArguments(message, ref offset);

            var resourcesCount = message.GetInt(offset++);
            for (int i = 0; i < resourcesCount; i++)
            {
                dto.Resources.Add(TowerResourceDTO.FromMessageArguments(message, ref offset));
            }

            dto.Hand = DeckDTO.FromMessageArguments(message, ref offset);

            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("PlayerIndex", PlayerIndex);
            dbObject.Set("PlayerName", PlayerName);
            dbObject.Set("ControlMode", (int) ControlMode);
            dbObject.Set("CurrentActionLogIndex", CurrentActionLogIndex);
            dbObject.Set("CurrentTurn", CurrentTurn);
            dbObject.Set("CardsDrawn", CardsDrawn);
            dbObject.Set("CardsPlayed", CardsPlayed);
            dbObject.Set("CurrentTowerSegment", CurrentTowerSegment != null 
                ? CurrentTowerSegment.ToDBObject()
                : new DatabaseObject()
            );
            dbObject.Set("Leader", Leader.ToDBObject());

            DatabaseArray resourcesDB = new DatabaseArray();
            if (Resources != null)
            {
                foreach (var resource in Resources)
                {
                    resourcesDB.Add(resource.ToDBObject());
                }
            }
            dbObject.Set("Resources", resourcesDB);

            dbObject.Set("Hand", Hand.ToDBObject());

            return dbObject;
        }

        public new static PlayerDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            PlayerDTO dto = new PlayerDTO();
            dto.PlayerIndex = dbObject.GetInt("PlayerIndex");
            dto.PlayerName = dbObject.GetString("PlayerName");
            dto.ControlMode = (ControlMode) dbObject.GetInt("ControlMode");
            dto.CurrentActionLogIndex = dbObject.GetInt("CurrentActionLogIndex");
            dto.CurrentTurn = dbObject.GetInt("CurrentTurn");
            dto.CardsDrawn = dbObject.GetInt("CardsDrawn");
            dto.CardsPlayed = dbObject.GetInt("CardsPlayed");
            dto.CurrentTowerSegment = TowerSegmentDTO.FromDBObject(dbObject.GetObject("CurrentTowerSegment"));
            dto.Leader = LeaderDTO.FromDBObject(dbObject.GetObject("Leader"));

            var resourcesDB = dbObject.GetArray("Resources");
            foreach (object resource in resourcesDB)
            {
                dto.Resources.Add(TowerResourceDTO.FromDBObject((DatabaseObject)resource));
            }

            dto.Hand = DeckDTO.FromDBObject(dbObject.GetObject("Hand"));

            return dto;
        }
    }
}
