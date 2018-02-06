using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class DeckDTO : DTO<DeckDTO>
    {
        public int DeckSize { get; set; }
        public List<CardType> Cards { get; set; }

        public DeckDTO()
        {
            Cards = new List<CardType>();
        }

        public override object[] ToMessageArguments(ref object[] args)
        {
           return new object[0];
        }

        public Message ToMessage(Message message)
        {
            message.Add(DeckSize);

            foreach (var card in Cards)
            {
                message.Add((int) card);
            }

            return message;
        }

        public static DeckDTO FromMessageArguments(Message message, ref uint offset)
        {
            DeckDTO dto = new DeckDTO();
            dto.DeckSize = message.GetInt(offset++);

            while (offset <= dto.DeckSize)
            {
                dto.Cards.Add((CardType) message.GetInt(offset++));
            }

            return dto;
        }
    }
}
