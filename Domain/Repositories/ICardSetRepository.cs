using CardVault.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardVault.Domain.Repositories
{
    public interface ICardSetRepository
    {
        public Task<CardSet?> GetByCodeAsync(string code);
        public Task<CardSet> AddAsync(CardSet cardSet);
    }
}
