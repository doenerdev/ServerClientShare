using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
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
