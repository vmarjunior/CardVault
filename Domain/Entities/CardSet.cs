using CardVault.Domain.Enums;

namespace CardVault.Domain.Entities
{
    public class CardSet
    {
        private CardSet() { }

        public CardSet(string code, string name, string imageUrl, DateTime? releaseDate, SetType setType, int cardsCount)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new InvalidOperationException("Set code cannot be empty.");

            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Set name cannot be empty.");

            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new InvalidOperationException("Image url cannot be empty.");

            if (cardsCount < 0)
                throw new InvalidOperationException("Cards count cannot be negative.");

            Id = Guid.NewGuid();
            Code = code.ToUpper().Trim();
            Name = name.Trim();
            ImageUrl = imageUrl.Trim();
            ReleaseDate = releaseDate;
            SetType = setType;
            CardsCount = cardsCount;
        }

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
        public DateTime? ReleaseDate { get; private set; }
        public SetType SetType { get; private set; }
        public int CardsCount { get; private set; }

        private readonly List<Card> _cards = new();
        public virtual IReadOnlyCollection<Card> Cards => _cards.AsReadOnly();
    }
}