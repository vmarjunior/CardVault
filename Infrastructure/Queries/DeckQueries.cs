using CardVault.Application.DTOs.Deck;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Mappers;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Queries
{
    public class DeckQueries(AppDbContext context) : IDeckQueries
    {
        public async Task<PagedResult<DeckListResponseDTO>> GetAll(DeckQueryParameters queryParams)
        {
            IQueryable<Deck> query = context.Decks.AsNoTracking();

            if (!string.IsNullOrEmpty(queryParams.DeckName))
                query = query.Where(x => EF.Functions.ILike(x.Name, $"%{queryParams.DeckName}%"));

            if (queryParams.DeckType is not null)
                query = query.Where(x => x.Type == queryParams.DeckType);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Name)
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(x => x.ToListDto())
                .ToListAsync();

            return new PagedResult<DeckListResponseDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize
            };
        }
    }
}
