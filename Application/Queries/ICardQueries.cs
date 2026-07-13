using CardVault.Application.DTOs.Card;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.QueryParameters;

namespace CardVault.Application.Queries
{
    public interface ICardQueries
    {
        public Task<PagedResult<CardResponseDTO>> GetAll(CardQueryParameters queryParams);
    }
}
