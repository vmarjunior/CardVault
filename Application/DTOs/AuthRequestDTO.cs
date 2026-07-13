using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.DTOs.Auth
{
    public class AuthRequestDTO
    {
        [Required]
        public string AccountName { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
