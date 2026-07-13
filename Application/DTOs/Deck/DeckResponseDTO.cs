using CardVault.Application.DTOs.Card;
using CardVault.Domain.Enums;

namespace CardVault.Application.DTOs.Deck
{
    public class DeckResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DeckType Type { get; set; }

        public List<CardResponseDTO> Cards { get; init; } = [];
    }
}