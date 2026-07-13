using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.QueryParameters
{
    public class CardQueryParameters
    {
        public string? CardName { get; set; }
        public string? CardSetName { get; set; }

        [MinLength(3)]
        public string? CardSetCode { get; set; }


        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 50)]
        public int PageSize { get; set;  } = 10;
    }
}
