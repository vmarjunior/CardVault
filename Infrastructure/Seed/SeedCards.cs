#pragma warning disable CS8618

using CardVault.Domain.Entities;
using CardVault.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CardVault.Infrastructure.Seed
{
    public static class SeedCards
    {
        private class CardSeedDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ImageUrl { get; set; }
            public string Artist { get; set; }
            public int ColorIdentity { get; set; }
            public string Supertype { get; set; }
            public string? Subtype { get; set; }
            public Guid SetId { get; set; }
            public int Rarity { get; set; }
            public bool IsLegendary { get; set; }
            public int ManaValue { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public DateTime PriceLastUpdated { get; set; }
            public int? Power { get; set; }
            public int? Toughness { get; set; }
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            var cardData = File.ReadAllText("../Infrastructure/Seed/Data/CardSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var cardsDto = JsonSerializer.Deserialize<List<CardSeedDto>>(cardData, options);

            if (cardsDto == null || !cardsDto.Any()) return;

            var cardsToSeed = cardsDto.Select(dto => new
            {
                Id = dto.Id,
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
                Artist = dto.Artist,
                ColorIdentity = (ColorIdentity)dto.ColorIdentity,
                Supertype = dto.Supertype,
                Subtype = dto.Subtype,
                Rarity = (Rarity)dto.Rarity,
                IsLegendary = dto.IsLegendary,
                ManaValue = dto.ManaValue,
                Description = dto.Description,
                Price = dto.Price,
                PriceLastUpdated = dto.PriceLastUpdated,
                Power = dto.Power,
                Toughness = dto.Toughness,
                SetId = dto.SetId
            });

            modelBuilder.Entity<Card>().HasData(cardsToSeed);
        }
    }
}