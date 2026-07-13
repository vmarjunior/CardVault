namespace CardVault.Domain.Entities
{
    public class UserCard
    {
        private UserCard() { }

        public UserCard(User user, Card card, Deck? deck = null)
        {
            Id = Guid.NewGuid(); 
            User = user ?? throw new ArgumentNullException(nameof(user), "User cannot be null.");
            Card = card ?? throw new ArgumentNullException(nameof(card), "Card cannot be null.");
            Deck = deck;

            //By default, a UserCard is not virtual.
            IsVirtual = false;
        }

        public Guid Id { get; private set; }
        public User User { get; private set; }
        public virtual Card Card { get; private set; }
        public Deck? Deck { get; private set; }

        //For drafts, proxies and ideas with cards that don't have a real card associated with them.
        public bool IsVirtual { get; private set; }

        public void AssignToDeck(Deck deck)
        {
            if (deck == null)
                throw new ArgumentNullException(nameof(deck), "Deck cannot be null.");

            this.Deck = deck;
        }

        public void RemoveFromDeck()
        {
            Deck = null;
        }

        public void ChangeVirtualStatus()
        {
            IsVirtual = !IsVirtual;
        }

        public static UserCard CreateVirtualCard(User user, Card card, Deck deck)
        {
            if (deck == null) throw new ArgumentNullException(nameof(deck));

            var virtualCard = new UserCard(user, card, deck);
            virtualCard.IsVirtual = true;
            return virtualCard;
        }
    }
}