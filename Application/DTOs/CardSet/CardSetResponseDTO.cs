using CardVault.Domain.Enums;

namespace CardVault.Application.DTOs.CardSet
{
    public class CardSetResponseDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime? ReleaseDate { get; set; }
        public SetType SetType { get; set; }
        public int CardsCount { get; set; }
    }
}
