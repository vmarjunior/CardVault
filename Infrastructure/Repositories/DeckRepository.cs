using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Repositories
{
    public class DeckRepository(AppDbContext context) : IDeckRepository
    {
        public async Task<Deck> AddAsync(Deck deck)
        {
            context.Decks.Add(deck);
            await context.SaveChangesAsync();
            return deck;
        }

        public async Task<Deck?> GetByIdAsync(Guid id)
        {
            return await context.Decks.FindAsync(id);
        }

        public async Task<ICollection<Deck>> GetByUserIdAsync(Guid userId)
        {
            return await context.Decks
                .Include(x => x.UserCards)
                .Where(x => x.User.Id == userId)
                .ToListAsync();
        }

        public async Task RemoveAsync(Deck deck)
        {
            context.Decks.Remove(deck);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Deck deck)
        {
            context.Decks.Update(deck);
            await context.SaveChangesAsync();
        }
    }
}
