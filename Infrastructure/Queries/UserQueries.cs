using Microsoft.EntityFrameworkCore;
using CardVault.Application.DTOs.User;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CardVault.Infrastructure.Queries
{
    public class UserQueries(AppDbContext context) : IUserQueries
    {
        public async Task<PagedResult<UserViewDTO>> GetAll(UserQueryParameters queryParams)
        {
            IQueryable<User> query = context.Users.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(userEntity => new UserViewDTO
                {
                    Id = userEntity.Id,
                    Nickname = userEntity.Nickname,
                    DeckCount = userEntity.Decks.Count(),
                    CardCount = userEntity.UserCards.Count(),
                    Created = userEntity.Created,
                    LastActive = userEntity.LastActive
                }).ToListAsync();

            return new PagedResult<UserViewDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize
            };
        }
    }
}