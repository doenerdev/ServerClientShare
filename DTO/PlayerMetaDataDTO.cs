using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.DTO;


namespace ServerClientShare.DTO
{
    public class PlayerMetaDataDTO : DatabaseDTO<PlayerMetaDataDTO>
    {
        public string PlayerName;
        public int PlayerIndex;
        public int Score;
        public bool IsOnline;
        public LeaderMetaDataDTO Leader;

        public override Message ToMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("PlayerName", PlayerName);
            dbObject.Set("PlayerIndex", PlayerIndex);
            dbObject.Set("Score", Score);
            dbObject.Set("IsOnline", IsOnline);
            dbObject.Set("Leader", Leader.ToDBObject());
            return dbObject;
        }

        public new static PlayerMetaDataDTO FromDBObject(DatabaseObject dbObject)
        {
            var dto = new PlayerMetaDataDTO();
            dto.PlayerName = dbObject.GetString("PlayerName");
            dto.PlayerIndex = dbObject.GetInt("PlayerIndex");
            dto.Score = dbObject.GetInt("Score");
            dto.IsOnline = dbObject.GetBool("IsOnline");
            dto.Leader = LeaderMetaDataDTO.FromDBObject(dbObject.GetObject("Leader"));
            return dto;
        }
    }
}
