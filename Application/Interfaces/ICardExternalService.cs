using CardVault.Domain.Entities;

namespace CardVault.Application.Interfaces
{
    public interface ICardExternalService
    {
        public Task<Card?> GetCardAsync(string name, string? setCode);
    }
}