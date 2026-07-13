using CardVault.Domain.Enums;

namespace CardVault.Application.DTOs.Deck
{
    public class DeckListResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DeckType Type { get; set; }
    }
}
