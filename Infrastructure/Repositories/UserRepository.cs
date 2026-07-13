using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await context.Users
                .Include(x => x.UserCards)
                .Include(x => x.Decks)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByAccountNameAsync(string accountName)
        {
            return await context.Users
                .FirstOrDefaultAsync(user => user.AccountName == accountName);
        }

        public async Task<User> AddAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsAccountNameTakenAsync(Guid? id, string accountName)
        {
            return await context.Users
                .AnyAsync(user => user.AccountName == accountName && (!id.HasValue || user.Id != id.Value));
        }
    }
}