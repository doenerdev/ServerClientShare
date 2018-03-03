using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;

namespace ServerClientShare.DTO
{
    public class LeaderDTO : DatabaseDTO<LeaderDTO>
    {
        public string Name { get; set; }
        public LeaderSkillDTO PrimarySkill { get; set; }
        public List<ActiveLeaderSkillDTO> SecondarySkills { get; set; }

        public LeaderDTO()
        {
            SecondarySkills = new List<ActiveLeaderSkillDTO>();
        }

        public override Message ToMessage(Message message)
        {
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
