using CardVault.Application.DTOs.Deck;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.QueryParameters;

namespace CardVault.Application.Queries
{
    public interface IDeckQueries
    {
        public Task<PagedResult<DeckListResponseDTO>> GetAll(DeckQueryParameters queryParams);
    }
}
