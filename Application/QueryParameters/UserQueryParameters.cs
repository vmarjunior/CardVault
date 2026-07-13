using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.QueryParameters
{
    public class UserQueryParameters
    {
        public string? Nickname { get; set; }

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 50)]
        public int PageSize { get; set; } = 10;
    }
}