using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;

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
