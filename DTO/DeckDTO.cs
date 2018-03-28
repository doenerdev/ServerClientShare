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
    [Serializable]
    public class DeckDTO : DatabaseDTO<DeckDTO>
    {
        public int DeckSize { get; set; }
        public List<CardDTO> Cards { get; set; }

        public DeckDTO()
        {
            Cards = new List<CardDTO>();
        }

        public override Message ToMessage(Message message)
        {
            message.Add(DeckSize);

            foreach (var card in Cards)
            {
                message = card.ToMessage(message);
            }

            return message;
        }

        public new static DeckDTO FromMessageArguments(Message message, ref uint offset)
        {
            DeckDTO dto = new DeckDTO();
            dto.DeckSize = message.GetInt(offset++);

            for(int i = 0; i < dto.DeckSize; i++) { 
                dto.Cards.Add(CardDTO.FromMessageArguments(message, ref offset));
            }

            return dto;
        }

        public override DatabaseObject ToDBObject()
        {
            DatabaseObject dbObject = new DatabaseObject();
            dbObject.Set("DeckSize", DeckSize);

            DatabaseArray cardsDB = new DatabaseArray();
            if (Cards != null)
            {
                foreach (var card in Cards)
                {
                    cardsDB.Add(card.ToDBObject());
                }
            }
            dbObject.Set("Cards", cardsDB);

            return dbObject;
        }

        public new static DeckDTO FromDBObject(DatabaseObject dbObject)
        {
            if (dbObject.Count == 0) return null;

            DeckDTO dto = new DeckDTO();
            dto.DeckSize = dbObject.GetInt("DeckSize");
            var cardsDB = dbObject.GetArray("Cards");

            for (int i = 0; i < dto.DeckSize; i++)
            {
                dto.Cards.Add(CardDTO.FromDBObject((DatabaseObject)cardsDB[i]));
            }

            return dto;
        }
    }
}
