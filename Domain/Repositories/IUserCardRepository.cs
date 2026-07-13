using CardVault.Domain.Entities;

namespace CardVault.Domain.Repositories
{
    public interface IUserCardRepository
    {
        public Task<UserCard?> GetByIdAsync(Guid id);
        public Task<UserCard> AddAsync(UserCard userCard);
        public Task UpdateAsync(UserCard userCard);
        public Task RemoveAsync(UserCard userCard);
    }
}
