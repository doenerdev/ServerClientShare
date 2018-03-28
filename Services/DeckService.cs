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
        public const int DeckSize = 30;
        public const int MarketplaceSize = 4;

        private DeckDTO _deck;
        private DeckDTO _marketplace;
        private ServerClientShare.Helper.RandomGenerator _rndGenerator;

        public DeckDTO Deck
        {
            get
            {
                if (_deck == null)
                {
                    Console.WriteLine("Generate Marketplace");
                    _deck = GenerateDeck(DeckSize);
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
                    _marketplace = GenerateDeck(MarketplaceSize);
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
            _deck = DeckDTO.FromDBObject(dbObject.GetObject("Marketplace"));
            _marketplace = DeckDTO.FromDBObject(dbObject.GetObject("Marketplace"));
        }

        public DeckService(DeckDTO marketplaceDto, DeckDTO deckDto, ServerClientShare.Helper.RandomGenerator rndGenerator) : this(rndGenerator)
        {
            _deck = deckDto;
            _marketplace = marketplaceDto;
        }

        private DeckDTO GenerateDeck(int size)
        {
            var dto = new DeckDTO();
            dto.DeckSize = size;

            for (int i = 0; i < dto.DeckSize; i++)
            {
                CardType cardType = CardType.CreateBuilder;
                var rnd = _rndGenerator.RandomRange(0D, 1D);
                if (rnd > 0.75D)
                {
                    cardType = CardType.CreateBuilder;
                }
                else if (rnd > 0.4D)
                {
                    cardType = CardType.CreateFighter;
                }
                else if (rnd > 0.28D)
                {
                  //  cardType = CardType.Gift;
                }
                else
                {
                    if (rnd > 0.2D)
                    {
                        cardType = CardType.Movement3;
                    }
                    else if (rnd > 0.1D)
                    {
                        cardType = CardType.Movement2;
                    }
                    else
                    {
                        cardType = CardType.Movement1;
                    }
                }
                dto.Cards.Add(new CardDTO() { CardType = cardType });
            }

            return dto;
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
