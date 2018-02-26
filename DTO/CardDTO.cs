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
    public class CardDTO : DatabaseDTO<CardDTO>
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

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("CardType", (int) CardType);
            dbObject.Set("Id", Id);
            return dbObject;
        }

        public new static CardDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            CardDTO dto = new CardDTO();
            dto.CardType = (CardType) dbObject.GetInt("CardType");
            dto.Id = dbObject.GetString("Id");
            return dto;
        }
    }
}
