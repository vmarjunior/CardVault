using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Repositories
{
    public class UserCardRepository(AppDbContext context) : IUserCardRepository
    {
        public async Task<UserCard?> GetByIdAsync(Guid id)
        {
            return await context.UserCards
                    .Include(x => x.Card)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserCard> AddAsync(UserCard userCard)
        {
            context.UserCards.Add(userCard);
            await context.SaveChangesAsync();
            return userCard;
        }

        public async Task UpdateAsync(UserCard userCard)
        {
            context.UserCards.Update(userCard);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(UserCard userCard)
        {
            context.UserCards.Remove(userCard);
            await context.SaveChangesAsync();
        }
    }
}
