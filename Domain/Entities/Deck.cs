using CardVault.Domain.Enums;

namespace CardVault.Domain.Entities
{
    public class Deck
    {
        private Deck() { }

        public Deck(string name, DeckType type, User user, bool isPrivate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Deck name cannot be empty.");

            User = user ?? throw new ArgumentNullException(nameof(user), "User cannot be null.");

            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            IsPrivate = isPrivate;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DeckType Type { get; private set; }
        public virtual User User { get; private set; }
        public bool IsPrivate { get; private set; }

        private readonly List<UserCard> _userCards = new();
        public virtual IReadOnlyCollection<UserCard> UserCards => _userCards.AsReadOnly();

        public decimal GetDeckPrice()
        {
            return _userCards.Sum(dc => dc.Card?.Price ?? 0);
        }

        public void UpdateDeckInformation(string name, DeckType deckType, bool isPrivate)
        {
            Name = name;
            Type = deckType;
            IsPrivate = isPrivate;
        }

        public void AddCard(UserCard userCard)
        {
            if (userCard is null)
                throw new ArgumentNullException(nameof(userCard));

            AddCards(new[] { userCard });
        }

        public void AddCards(IEnumerable<UserCard> userCards)
        {
            if (userCards is null)
                throw new ArgumentNullException(nameof(userCards));

            foreach (var userCard in userCards)
            {
                userCard.AssignToDeck(this);
                _userCards.Add(userCard);
            }
        }

        public void RemoveCard(UserCard userCard)
        {
            if (userCard is null)
                throw new ArgumentNullException(nameof(userCard));

            userCard.RemoveFromDeck();
            _userCards.Remove(userCard);
        }
    }
}