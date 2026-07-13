using CardVault.Application.DTOs.Deck;
using CardVault.Domain.Entities;

namespace CardVault.Application.Mappers
{
    public static class DeckMapper
    {
        public static DeckResponseDTO ToDto(this Deck deck)
        {
            return new DeckResponseDTO
            {
                Id = deck.Id,
                Name = deck.Name,
                Type = deck.Type,
                Cards = deck.UserCards.Select(x => x.Card.ToDto()).ToList()
            };
        }
    }
}