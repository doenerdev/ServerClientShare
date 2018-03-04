using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using ServerClientShare.DTO;
using ServerClientShare.Enums;

namespace ServerClientShare.Services
{
    public class LeaderService
    {
        public LeaderDTO CreateLeader(LeaderType type)
        {
            LeaderDTO dto = null;

            switch (type)
            {
                case LeaderType.Dwarf:
                case LeaderType.Elf:
                    dto = new LeaderDTO()
                    {
                        Type = type,
                        Name = "Dwarf", //TODO get all this data from the db
                        PrimarySkill = new LeaderSkillDTO()
                        {
                            Type = LeaderSkillType.MarchingOrders
                        },
                        SecondarySkills = new List<ActiveLeaderSkillDTO>()
                        {
                            new ActiveLeaderSkillDTO()
                            {
                                Type = LeaderSkillType.MarchingOrders,
                                CoolDown = 2
                            },
                            new ActiveLeaderSkillDTO()
                            {
                                Type = LeaderSkillType.Masonry,
                                CoolDown = 2
                            },
                            new ActiveLeaderSkillDTO()
                            {
                                Type = LeaderSkillType.Riot,
                                CoolDown = 2
                            },
                        }
                    };
                    break;
            }

            return dto;
        }
    }
}
