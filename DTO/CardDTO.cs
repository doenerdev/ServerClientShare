using System;
using System.Collections.Generic;
using System.Text;
using PlayerIO.GameLibrary;
using ServerClientShare.Enums;

namespace ServerClientShare.DTO
{
    public class CardDTO : DTO<CardDTO>
    {
        public CardType CardType { get; set; }
        public string Id { get; set; }

        public CardDTO()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override Message ToMessage(Message message)
        {
            message.Add(Id);
            message.Add((int) CardType);
            return message;
        }

        public new static CardDTO FromMessageArguments(Message message, ref uint offset)
        {
            CardDTO dto = new CardDTO();
            dto.Id = message.GetString(offset++);
            dto.CardType = (CardType) message.GetInt(offset++);
            return dto;
        }
    }
}
