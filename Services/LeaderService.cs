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
                    dto = new LeaderDTO()
                    {
                        Type = type,
                        Name = "Dwarf", //TODO get all this data from the db
                        PrimarySkill = new LeaderSkillDTO()
                        {
                            Type = LeaderSkillType.DwarvenMainPower
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
                case LeaderType.Elf:
                    dto = new LeaderDTO()
                    {
                        Type = type,
                        Name = "Elf", //TODO get all this data from the db
                        PrimarySkill = new LeaderSkillDTO()
                        {
                            Type = LeaderSkillType.ElvenMainPower
                        },
                        SecondarySkills = new List<ActiveLeaderSkillDTO>()
                        {
                            new ActiveLeaderSkillDTO()
                            {
                                Type = LeaderSkillType.SecondSpring,
                                CoolDown = 2
                            },
                            new ActiveLeaderSkillDTO()
                            {
                                Type = LeaderSkillType.Handicraft,
                                CoolDown = 2
                            },
                            new ActiveLeaderSkillDTO()
                            {
                                Type = LeaderSkillType.GuerillaAttack,
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
