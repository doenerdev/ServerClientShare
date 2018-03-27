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

namespace ServerClientShare.DTO
{
    public class GameSessionTurnDataDTO : DatabaseDTO<GameSessionTurnDataDTO>
    {
        public MatchDTO Match { get; set; }
        public HexMapDTO HexMap { get; set; }
        public DeckDTO Marketplace { get; set; }
        public DeckDTO Deck { get; set; }

        public override Message ToMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject turnData = new DatabaseObject();
            turnData.Set("Match", Match.ToDBObject());
            turnData.Set("HexMap", HexMap.ToDBObject());
            turnData.Set("Marketplace", Marketplace.ToDBObject());
            turnData.Set("Deck", Deck.ToDBObject());
            return turnData;
        }

        public new static GameSessionTurnDataDTO FromDBObject(DatabaseObject dbObject)
        {
            var dto = new GameSessionTurnDataDTO();
            dto.Match = MatchDTO.FromDBObject(dbObject.GetObject("Match"));
            dto.HexMap = HexMapDTO.FromDBObject(dbObject.GetObject("HexMap"));
            dto.Marketplace = DeckDTO.FromDBObject(dbObject.GetObject("Marketplace"));
            dto.Deck = DeckDTO.FromDBObject(dbObject.GetObject("Deck"));
            return dto;
        }
    }
}
