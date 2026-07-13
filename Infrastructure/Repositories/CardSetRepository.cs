using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Repositories
{
    public class CardSetRepository(AppDbContext context) : ICardSetRepository
    {
        public async Task<CardSet> AddAsync(CardSet cardSet)
        {
            context.CardSets.Add(cardSet);
            await context.SaveChangesAsync();
            return cardSet;
        }

        public async Task<CardSet?> GetByCodeAsync(string code)
        {
            var cardSet = await context.CardSets.FirstOrDefaultAsync(c => c.Code.ToUpper() == code.ToUpper());
            return cardSet;
        }
    }
}
