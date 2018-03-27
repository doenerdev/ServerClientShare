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
    public class LeaderMetaDataDTO : DatabaseDTO<LeaderMetaDataDTO>
    {
        public LeaderType LeaderType { get; set; }
        public string Name { get; set; }

        public override Message ToMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public override DatabaseObject ToDBObject()
        {
            var dbObject = new DatabaseObject();
            dbObject.Set("LeaderType", (int) LeaderType);
            dbObject.Set("Name", Name);
            return dbObject;
        }

        public new static LeaderMetaDataDTO FromDBObject(DatabaseObject dbObject)
        {
            var dto = new LeaderMetaDataDTO();
            dto.LeaderType = (LeaderType) dbObject.GetInt("LeaderType");
            dto.Name = dbObject.GetString("Name");
            return dto;
        }
    }
}
