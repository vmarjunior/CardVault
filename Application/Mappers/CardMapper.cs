using CardVault.Application.DTOs.Card;
using CardVault.Domain.Entities;

namespace CardVault.Application.Mappers
{
    public static class CardMapper
    {
        public static CardResponseDTO ToDto(this Card card)
        {
            return new CardResponseDTO
            {
                Id = card.Id,
                Name = card.Name,
                ImageUrl = card.ImageUrl,
                Artist = card.Artist,
                ColorIdentity = card.ColorIdentity,
                Supertype = card.Supertype,
                Subtype = card.Subtype,
                Rarity = card.Rarity,
                IsLegendary = card.IsLegendary,
                ManaValue = card.ManaValue,
                Description = card.Description,
                Price = card.Price,
                PriceLastUpdated = card.PriceLastUpdated,
                Power = card.Power,
                Toughness = card.Toughness,
                CardSet = card.Set.ToDto()
            };
        }
    }
}