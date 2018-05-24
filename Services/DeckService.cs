using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL || UNITY_IOS || UNITY_IPHONE || UNITY_ANDROID || UNITY_WII || UNITY_PS4 || UNITY_SAMSUNGTV || UNITY_XBOXONE || UNITY_TIZEN || UNITY_TVOS || UNITY_WP_8_1 || UNITY_WSA || UNITY_WSA_8_1 || UNITY_WSA_10_0 || UNITY_WINRT || UNITY_WINRT_8_1 || UNITY_WINRT_10_0
using PlayerIOClient;
#else
using PlayerIO.GameLibrary;
#endif
using ServerClientShare.DTO;
using ServerClientShare.Enums;
using ServerClientShare.Helper;

namespace ServerClientShare.Services
{
    public class DeckService
    {
        public const int QtyBuilderCards = 9;
        public const int QtyFighterCards = 5;
        public const int QtyMovement1Cards = 5;
        public const int QtyMovement3Cards = 3;
        public const int QtyGiftCards = 3;
        public const int MarketplaceSize = 4;
        public const int QtyDeckStacks = 5;

        private DeckDTO _deck;
        private DeckDTO _marketplace;
        private ServerClientShare.Helper.RandomGenerator _rndGenerator;

        public int DeckSize
        {
            get
            {
                return QtyBuilderCards + QtyFighterCards + QtyMovement1Cards
                       +  QtyMovement3Cards + QtyGiftCards;
            }
        }
        public DeckDTO Deck
        {
            get
            {
                if (_deck == null)
                {
                    Console.WriteLine("Generate Marketplace");
                    _deck = GenerateDeck();
                }
                return _deck;
            }
        }

        public DeckDTO Marketplace
        {
            get
            {
                if (_marketplace == null)
                {
                    Console.WriteLine("Generate Marketplace");
                    if (_deck == null)
                        _deck = GenerateDeck();

                    _marketplace = new DeckDTO() { DeckSize = MarketplaceSize};
                    for (int i = 0; i < MarketplaceSize; i++)
                    {
                        _marketplace.Cards.Add(_deck.Cards[i]);
                    }
                    for (int i = 0; i < MarketplaceSize; i++)
                    {
                        _deck.Cards.RemoveAt(i);
                    }
                    _deck.DeckSize -= MarketplaceSize;
                }
                return _marketplace;
            }
        }

        public DeckService(ServerClientShare.Helper.RandomGenerator rndGenerator)
        {
           _rndGenerator = rndGenerator;
        }

        public DeckService(DatabaseObject dbObject, ServerClientShare.Helper.RandomGenerator rndGenerator) : this(rndGenerator)
        {
            _deck = DeckDTO.FromDBObject(dbObject.GetObject("Deck"));
            _marketplace = DeckDTO.FromDBObject(dbObject.GetObject("Marketplace"));
        }

        public DeckService(DeckDTO marketplaceDto, DeckDTO deckDto, ServerClientShare.Helper.RandomGenerator rndGenerator) : this(rndGenerator)
        {
            _deck = deckDto;
            _marketplace = marketplaceDto;
        }

        private DeckDTO GenerateDeck()
        {
            var dto = new DeckDTO();

            List<CardDTO> deckCards = new List<CardDTO>();
            for (int j = 0; j < QtyDeckStacks; j++)
            {
                List<CardDTO> cards = new List<CardDTO>();
                for (int i = 0; i < QtyFighterCards; i++)
                    cards.Add(new CardDTO() { CardType = CardType.CreateFighter });

                for (int i = 0; i < QtyBuilderCards; i++)
                    cards.Add(new CardDTO() { CardType = CardType.CreateBuilder });

                for (int i = 0; i < QtyMovement1Cards; i++)
                    cards.Add(new CardDTO() { CardType = CardType.Movement1 });

                for (int i = 0; i < QtyMovement3Cards; i++)
                    cards.Add(new CardDTO() { CardType = CardType.Movement3 });

                for (int i = 0; i < QtyGiftCards; i++)
                    cards.Add(new CardDTO() { CardType = CardType.Gift });

                Shuffle(cards);
                deckCards.AddRange(cards);
            }
            dto.Cards = deckCards;
            dto.DeckSize = deckCards.Count;
           
            return dto;
        }

        public DeckDTO Shuffle(DeckDTO dto)
        {
            dto.Cards = Shuffle(dto.Cards);
            return dto;
        }

        public List<CardDTO> Shuffle(List<CardDTO> cards)
        {
            //https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            //using Fisher-Yates Shuffle

            for (int i = cards.Count - 1; i > 0; i--)
            {
                var rnd = _rndGenerator.RandomRange(0, cards.Count);
                var buffer = cards[rnd];
                cards[rnd] = cards[i];
                cards[i] = buffer;
            }
            return cards;
        }

        public void UpdateMarketplace(DeckDTO dto)
        {
            _marketplace = dto;
        }

        public void UpdateDeck(DeckDTO dto)
        {
            _deck = dto;
        }

    }
}
