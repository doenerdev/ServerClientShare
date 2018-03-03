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
    public class PlayerDTO : DatabaseDTO<PlayerDTO>
    {
        public int PlayerIndex { get; set; }
        public string PlayerName { get; set; }
        public ControlMode ControlMode { get; set; }
        public TowerSegmentDTO CurrentTowerSegment { get; set; }
        public LeaderDTO Leader { get; set; }

        public override Message ToMessage(Message message)
        {
            message.Add(PlayerIndex);
            message.Add(PlayerName);
            message.Add((int) ControlMode);
            message = CurrentTowerSegment.ToMessage(message);
            message = Leader.ToMessage(message);
            return message;
        }

        public new static PlayerDTO FromMessageArguments(Message message, ref uint offset)
        {
            PlayerDTO dto = new PlayerDTO();
            dto.PlayerIndex = message.GetInt(offset++);
            dto.PlayerName = message.GetString(offset++);
            dto.ControlMode = (ControlMode) message.GetInt(offset++);
            dto.CurrentTowerSegment = TowerSegmentDTO.FromMessageArguments(message, ref offset);
            dto.Leader = LeaderDTO.FromMessageArguments(message, ref offset);
            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("PlayerIndex", PlayerIndex);
            dbObject.Set("PlayerName", PlayerName);
            dbObject.Set("ControlMode", (int) ControlMode);
            dbObject.Set("CurrentTowerSegment", CurrentTowerSegment != null 
                ? CurrentTowerSegment.ToDBObject()
                : new DatabaseObject()
            );
            dbObject.Set("Leader", Leader.ToDBObject());

            return dbObject;
        }

        public new static PlayerDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            PlayerDTO dto = new PlayerDTO();
            dto.PlayerIndex = dbObject.GetInt("PlayerIndex");
            dto.PlayerName = dbObject.GetString("PlayerName");
            dto.ControlMode = (ControlMode) dbObject.GetInt("ControlMode");
            dto.CurrentTowerSegment = TowerSegmentDTO.FromDBObject(dbObject.GetObject("CurrentTowerSegment"));
            dto.Leader = LeaderDTO.FromDBObject(dbObject.GetObject("Leader"));

            return dto;
        }
    }
}
