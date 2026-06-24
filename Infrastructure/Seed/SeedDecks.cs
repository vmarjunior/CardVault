#pragma warning disable CS8618

using CardVault.Domain.Entities;
using CardVault.Domain.Enums;
using System.Text.Json;

namespace CardVault.Infrastructure.Seed
{
    public static class SeedDecks
    {
        private class DeckSeedDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Type { get; set; }
            public Guid UserId { get; set; }
            public bool IsPrivate { get; set; }
        }

        public static void Seed(AppDbContext context)
        {
            if (context.Decks.Any()) return;

            var deckData = File.ReadAllText("../Infrastructure/Seed/Data/DeckSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dtos = JsonSerializer.Deserialize<List<DeckSeedDto>>(deckData, options);

            if (dtos == null || !dtos.Any()) return;

            var users = context.Users.ToList();

            foreach (var dto in dtos)
            {
                var user = users.First(u => u.Id == dto.UserId);
                var deck = new Deck(dto.Name, (DeckType)dto.Type, user, dto.IsPrivate);

                context.Entry(deck).Property(d => d.Id).CurrentValue = dto.Id;

                context.Decks.Add(deck);
            }
            context.SaveChanges();
        }
    }
}