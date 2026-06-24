#pragma warning disable CS8618

using CardVault.Domain.Entities;
using System.Text.Json;

namespace CardVault.Infrastructure.Seed
{
    public static class SeedUserCards
    {
        private class UserCardSeedDto
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public Guid CardId { get; set; }
            public Guid? DeckId { get; set; }
            public bool IsVirtual { get; set; }
        }

        public static void Seed(AppDbContext context)
        {
            if (context.UserCards.Any()) return;

            var userCardData = File.ReadAllText("../Infrastructure/Seed/Data/UserCardSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dtos = JsonSerializer.Deserialize<List<UserCardSeedDto>>(userCardData, options);

            if (dtos == null || !dtos.Any()) return;

            var users = context.Users.ToList();
            var cards = context.Cards.ToList();
            var decks = context.Decks.ToList();

            foreach (var dto in dtos)
            {
                var user = users.First(u => u.Id == dto.UserId);
                var card = cards.First(c => c.Id == dto.CardId);
                var deck = dto.DeckId.HasValue ? decks.First(d => d.Id == dto.DeckId.Value) : null;

                UserCard userCard;

                if (dto.IsVirtual)
                {
                    userCard = UserCard.CreateVirtualCard(user, card, deck!);
                }
                else
                {
                    userCard = new UserCard(user, card, deck);
                }

                context.Entry(userCard).Property(uc => uc.Id).CurrentValue = dto.Id;
                context.UserCards.Add(userCard);
            }
            context.SaveChanges();
        }
    }
}