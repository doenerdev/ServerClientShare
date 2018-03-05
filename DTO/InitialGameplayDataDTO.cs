using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif

namespace ServerClientShare.DTO
{
    public class InitialGameplayDataDTO : DatabaseDTO<InitialGameplayDataDTO>
    {
        public MatchDTO Match { get; set; }
        public HexMapDTO HexMap { get; set; }
        public DeckDTO Marketplace { get; set; }
        public DeckDTO Deck { get; set; }
        public PlayerActionsLogDTO ActionLog { get; set; }

        public override Message ToMessage(Message message)
        {
            message = Match.ToMessage(message);
            message = HexMap.ToMessage(message);
            message = Marketplace.ToMessage(message);
            message = Deck.ToMessage(message);
            message = ActionLog.ToMessage(message);

            return message;
        }

        public new static InitialGameplayDataDTO FromMessageArguments(Message message, ref uint offset)
        {
            InitialGameplayDataDTO dto = new InitialGameplayDataDTO();
            dto.Match = MatchDTO.FromMessageArguments(message, ref offset);
            dto.HexMap = HexMapDTO.FromMessageArguments(message, ref offset);
            dto.Marketplace = DeckDTO.FromMessageArguments(message, ref offset);
            dto.Deck = DeckDTO.FromMessageArguments(message, ref offset);
            dto.ActionLog = PlayerActionsLogDTO.FromMessageArguments(message, ref offset);

            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("Match", Match.ToDBObject());
            dbObject.Set("HexMap", HexMap.ToDBObject());
            dbObject.Set("Marketplace", Marketplace.ToDBObject());
            dbObject.Set("Deck", Deck.ToDBObject());
            dbObject.Set("PlayerActionLog", ActionLog.ToDBObject());

            return dbObject;
        }

        public new static InitialGameplayDataDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            var dto = new InitialGameplayDataDTO
            {
                Match = MatchDTO.FromDBObject(dbObject.GetObject("Match")),
                HexMap = HexMapDTO.FromDBObject(dbObject.GetObject("HexMap")),
                Marketplace = DeckDTO.FromDBObject(dbObject.GetObject("Marketplace")),
                Deck = DeckDTO.FromDBObject(dbObject.GetObject("Deck")),
                ActionLog = PlayerActionsLogDTO.FromDBObject(dbObject.GetObject("PlayerActionLog"))
            };

            return dto;
        }
    }
}
