using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Repositories
{
    public class CardRepository(AppDbContext context) : ICardRepository
    {
        public async Task<Card?> GetByIdAsync(Guid id)
        {
            var card = await context.Cards
                .Include(x => x.Set)
                .FirstOrDefaultAsync(c => c.Id == id);

            return card;
        }

        public async Task<Card> AddAsync(Card card)
        {
            context.Cards.Add(card);
            await context.SaveChangesAsync();
            return card;
        }

        public async Task<Card?> GetByNameAndSetAsync(string name, string? setCode)
        {
            var card = await context.Cards.FirstOrDefaultAsync(c => c.Name == name && c.Set.Code == setCode);
            return card;
        }

        public async Task UpdateAsync(Card card)
        {
            context.Cards.Update(card);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Card card)
        {
            context.Cards.Remove(card);
            await context.SaveChangesAsync();
        }
    }
}
