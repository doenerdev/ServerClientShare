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
    public class LeaderSkillDTO : DatabaseDTO<LeaderSkillDTO>
    {
        public LeaderSkillType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override Message ToMessage(Message message)
        {
            message.Add((int) Type);
            message.Add(Name);
            message.Add(Description);
            return message;
        }

        public new static LeaderSkillDTO FromMessageArguments(Message message, ref uint offset)
        {
            LeaderSkillDTO dto = new LeaderSkillDTO();
            dto.Type = (LeaderSkillType) message.GetInt(offset++);
            dto.Name = message.GetString(offset++);
            dto.Description = message.GetString(offset++);
            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("Type", (int) Type);
            dbObject.Set("Name", Name);
            dbObject.Set("Description", Description);
            return dbObject;
        }

        public new static LeaderSkillDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            LeaderSkillDTO dto = new LeaderSkillDTO
            {
                Type = (LeaderSkillType) dbObject.GetInt("Type"),
                Name = dbObject.GetString("Name"),
                Description = dbObject.GetString("Description")
            };
            return dto;
        }
    }
}
