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
    public class ActiveLeaderSkillDTO : LeaderSkillDTO
    {
        public int CoolDown { get; set; }

        public override Message ToMessage(Message message)
        {
            message = base.ToMessage(message);
            message.Add(CoolDown);
            return message;
        }

        public new static ActiveLeaderSkillDTO FromMessageArguments(Message message, ref uint offset)
        {
            ActiveLeaderSkillDTO dto = new ActiveLeaderSkillDTO();
            dto.Type = (LeaderSkillType)message.GetInt(offset++);
            dto.Name = message.GetString(offset++);
            dto.Description = message.GetString(offset++);
            dto.CoolDown = message.GetInt(offset++);
            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("Type", (int)Type);
            dbObject.Set("Name", Name);
            dbObject.Set("Description", Description);
            dbObject.Set("CoolDown", CoolDown);
            return dbObject;
        }

        public new static ActiveLeaderSkillDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            ActiveLeaderSkillDTO dto = new ActiveLeaderSkillDTO
            {
                Type = (LeaderSkillType) dbObject.GetInt("Type"),
                Name = dbObject.GetString("Name"),
                Description = dbObject.GetString("Description"),
                CoolDown = dbObject.GetInt("CoolDown")
            };
            return dto;
        }
    }
}
