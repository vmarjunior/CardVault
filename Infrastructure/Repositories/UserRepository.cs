using Microsoft.EntityFrameworkCore;
using CardVault.Domain.Entities;
using CardVault.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace CardVault.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await context.Users
                .FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<User?> GetByAccountNameAsync(string accountName)
        {
            return await context.Users
                .FirstOrDefaultAsync(user => user.AccountName == accountName);
        }

        public async Task<User> CreateAsync(User user)
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

        public async Task DeleteAsync(Guid userId)
        {
            var userToDelete = await context.Users.FindAsync(userId);

            if (userToDelete == null)
                throw new InvalidOperationException($"Cannot delete user with ID {userId} because it was not found.");

            context.Users.Remove(userToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsAccountNameTakenAsync(Guid? id, string accountName)
        {
            return await context.Users
                .AnyAsync(user => user.AccountName == accountName && (!id.HasValue || user.Id != id.Value));
        }
    }
}