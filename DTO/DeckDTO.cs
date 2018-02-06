using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
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

            while (offset < dto.DeckSize)
            {
                dto.Cards.Add((CardType) message.GetInt(offset++));
            }

            return dto;
        }
    }
}
