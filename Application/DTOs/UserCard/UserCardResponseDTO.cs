namespace CardVault.Application.DTOs.UserCard
{
    public class UserCardResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string CardSet { get; set; } = default!;
        public decimal Price { get; set; }
    }
}