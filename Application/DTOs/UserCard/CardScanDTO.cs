namespace CardVault.Application.DTOs.UserCard
{
    public class CardScanDTO
    {
        public string Name { get; set; } = default!;
        public string? SetCode { get; set; }
        public Guid? DeckId { get; set; }
    }
}
