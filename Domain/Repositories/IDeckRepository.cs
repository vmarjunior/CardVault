using CardVault.Domain.Entities;

namespace CardVault.Domain.Repositories
{
    public interface IDeckRepository
    {
        public Task<Deck?> GetByIdAsync(Guid id);
        public Task<ICollection<Deck>> GetByUserIdAsync(Guid userId);
        public Task<Deck> AddAsync(Deck deck);
        public Task UpdateAsync(Deck deck);
        public Task RemoveAsync(Deck deck);
    }
}
