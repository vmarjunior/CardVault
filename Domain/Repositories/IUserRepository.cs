using CardVault.Domain.Entities;

namespace CardVault.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetByIdAsync(Guid userId);
        public Task<User?> GetByAccountNameAsync(string accountName);
        public Task<User> AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task RemoveAsync(User user);
        public Task<bool> IsAccountNameTakenAsync(Guid? id, string accountName);
    }
}
