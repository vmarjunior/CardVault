using System.ComponentModel.DataAnnotations;

namespace CardVault.Application.DTOs.Auth
{
    public class AuthRequestDTO
    {
        [Required]
        public string AccountName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
