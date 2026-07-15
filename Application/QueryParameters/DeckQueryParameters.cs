using CardVault.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.QueryParameters
{
    public class DeckQueryParameters
    {
        public string? DeckName { get; set; }
        public DeckType? DeckType { get; set; }


        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 50)]
        public int PageSize { get; set; } = 20;
    }
}
