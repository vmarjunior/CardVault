using CardVault.Application.DTOs.CardSet;
using CardVault.Domain.Enums;

namespace CardVault.Application.DTOs.Card
{
    public class CardResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public string Artist { get; set; } = default!;
        public string ColorIdentity { get; set; } = default!;
        public string Supertype { get; set; } = default!;
        public string? Subtype { get; set; } = default!;
        public Rarity Rarity { get; set; }
        public bool IsLegendary { get; set; }
        public int ManaValue { get; set; }
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime PriceLastUpdated { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }


        public virtual CardSetResponseDTO CardSet { get; set; } = default!;
    }
}
