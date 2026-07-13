using CardVault.Domain.Entities;

namespace CardVault.Domain.Repositories
{
    public interface ICardRepository
    {
        public Task<Card?> GetByIdAsync(Guid id);
        public Task<Card?> GetByNameAndSetAsync(string name, string? setCode);
        public Task<Card> AddAsync(Card card);
        public Task UpdateAsync(Card card);
        public Task RemoveAsync(Card card);
    }
}
