using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class PlayerDTO : DTO<PlayerDTO>
    {
        public int PlayerIndex { get; set; }
        public string PlayerName { get; set; }
        public PlayerType PlayerType { get; set; }

        public override Message ToMessage(Message message)
        {
            message.Add(PlayerIndex);
            message.Add(PlayerName);
            message.Add((int) PlayerType);
            return message;
        }

        public new static PlayerDTO FromMessageArguments(Message message, ref uint offset)
        {
            PlayerDTO dto = new PlayerDTO();
            dto.PlayerIndex = message.GetInt(offset++);
            dto.PlayerName = message.GetString(offset++);
            dto.PlayerType = (PlayerType) message.GetInt(offset++);
            return dto;
        }
    }
}
