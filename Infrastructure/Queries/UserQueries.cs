using CardVault.Application.DTOs.User;
using CardVault.Application.DTOs.Wrapper;
using CardVault.Application.Mappers;
using CardVault.Application.Queries;
using CardVault.Application.QueryParameters;
using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardVault.Infrastructure.Queries
{
    public class UserQueries(AppDbContext context) : IUserQueries
    {
        public async Task<PagedResult<UserResponseDTO>> GetAll(UserQueryParameters queryParams)
        {
            IQueryable<User> query = context.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(queryParams.Nickname))
                query = query.Where(x => EF.Functions.ILike(x.Nickname, $"%{queryParams.Nickname}%"));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(u => u.Id)
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(user => user.ToDto())
                .ToListAsync();

            return new PagedResult<UserResponseDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize
            };
        }
    }
}