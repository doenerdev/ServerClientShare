using System;
using System.Collections.Generic;
using System.Text;
using ServerClientShare.Enums;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif

namespace ServerClientShare.DTO
{
    public class LeaderDTO : DatabaseDTO<LeaderDTO>
    {
        public LeaderType Type { get; set; }
        public string Name { get; set; }
        public LeaderSkillDTO PrimarySkill { get; set; }
        public List<ActiveLeaderSkillDTO> SecondarySkills { get; set; }

        public LeaderDTO()
        {
            SecondarySkills = new List<ActiveLeaderSkillDTO>();
        }

        public override Message ToMessage(Message message)
        {
            message.Add((int) Type);
            message.Add(Name);
            message = PrimarySkill.ToMessage(message);
            message.Add(SecondarySkills.Count);

            foreach (var secondarySkill in SecondarySkills)
            {
                message = secondarySkill.ToMessage(message);
            }

            return message;
        }

        public new static LeaderDTO FromMessageArguments(Message message, ref uint offset)
        {
            LeaderDTO dto = new LeaderDTO();
            dto.Type = (LeaderType) message.GetInt(offset++);
            dto.Name = message.GetString(offset++);
            dto.PrimarySkill = LeaderSkillDTO.FromMessageArguments(message, ref offset);

            var secondarySkillsCount = message.GetInt(offset++);
            for (int i = 0; i < secondarySkillsCount; i++)
            {
                dto.SecondarySkills.Add(ActiveLeaderSkillDTO.FromMessageArguments(message, ref offset));
            }

            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("Type", (int) Type);
            dbObject.Set("Name", Name);
            dbObject.Set("PrimarySkill", PrimarySkill.ToDBObject());
            dbObject.Set("SecondarySkillsCount", SecondarySkills.Count);

            DatabaseArray secondarySkillsDB = new DatabaseArray();
            foreach (var secondarySkill in SecondarySkills)
            {
                secondarySkillsDB.Add(secondarySkill.ToDBObject());
            }
            dbObject.Set("SecondarySkills", secondarySkillsDB);

            return dbObject;
        }

        public new static LeaderDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            LeaderDTO dto = new LeaderDTO
            {
                Type = (LeaderType) dbObject.GetInt("Type"),
                Name = dbObject.GetString("Name"),
                PrimarySkill = LeaderSkillDTO.FromDBObject(dbObject.GetObject("PrimarySkill"))
            };

            var secondarySkillsDB = dbObject.GetArray("SecondarySkills");
            foreach (object secondarySkill in secondarySkillsDB)
            {
                dto.SecondarySkills.Add(ActiveLeaderSkillDTO.FromDBObject((DatabaseObject) secondarySkill));
            }

            return dto;
        }
    }
}
