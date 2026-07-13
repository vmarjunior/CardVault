using CardVault.Application.DTOs.Card;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Mappers;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Queries
{
    public class CardQueries(AppDbContext context) : ICardQueries
    {
        public async Task<PagedResult<CardResponseDTO>> GetAll(CardQueryParameters queryParams)
        {
            IQueryable<Card> query = context.Cards.AsNoTracking();

            if (!string.IsNullOrEmpty(queryParams.CardName))
                query = query.Where(x => EF.Functions.ILike(x.Name, $"%{queryParams.CardName}%"));

            if (!string.IsNullOrEmpty(queryParams.CardSetName))
                query = query.Where(x => EF.Functions.ILike(x.Set.Name, $"%{queryParams.CardSetName}%"));

            if (!string.IsNullOrEmpty(queryParams.CardSetCode))
                query = query.Where(x => x.Set.Code == queryParams.CardSetCode);

            var totalCount = await query.CountAsync();

            var items = await query
                .Include(x => x.Set)
                .OrderBy(x => x.Name)
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(x => x.ToDto())
                .ToListAsync();

            return new PagedResult<CardResponseDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize
            };
        }
    }
}
