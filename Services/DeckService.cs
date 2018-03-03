using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Console.WriteLine("Generate Deck");
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

        private DeckDTO GenerateDeck(int size)
        {
            var dto = new DeckDTO();
            dto.DeckSize = size;

            for (int i = 0; i < dto.DeckSize; i++)
            {
                CardType cardType = CardType.CreateBuilder;
                var rnd = _rndGenerator.RandomRange(0D, 1D);
                if (rnd > 0.65D)
                {
                    cardType = CardType.CreateBuilder;
                }
                else if (rnd > 0.3D)
                {
                    cardType = CardType.CreateFighter;
                }
                else
                {
                    cardType = CardType.Movement3;
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
