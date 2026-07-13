using CardVault.Application.DTOs.Deck;
using CardVault.Domain.Entities;

namespace CardVault.Application.Mappers
{
    public static class DeckListMapper
    {
        public static DeckListResponseDTO ToListDto(this Deck deck)
        {
            return new DeckListResponseDTO
            {
                Id = deck.Id,
                Name = deck.Name,
                Type = deck.Type,
            };
        }
    }
}
