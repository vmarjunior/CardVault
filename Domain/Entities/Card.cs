using CardVault.Domain.Enums;
using System;

namespace CardVault.Domain.Entities
{
    public class Card
    {
        private Card() { }

        public Card(string name, string imageUrl, string artist, string colorIdentity, string supertype, string subtype, CardSet set, Rarity rarity, bool isLegendary, int manaValue, string description, decimal price, int? power, int? toughness)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Card name cannot be empty.");

            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new InvalidOperationException("Card image 'imageUrl' cannot be empty.");

            if (set == null)
                throw new ArgumentNullException(nameof(set), "Card set cannot be null.");

            Id = Guid.NewGuid();
            Name = name;
            ImageUrl = imageUrl;
            Artist = artist;
            ColorIdentity = colorIdentity;
            Supertype = supertype;
            Subtype = subtype;
            Set = set;
            Rarity = rarity;
            IsLegendary = isLegendary;
            ManaValue = manaValue;
            Description = description;
            Power = power;
            Toughness = toughness;

            UpdateCardPrice(price);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }
        public string Artist { get; private set; }
        public string ColorIdentity { get; private set; }
        public string Supertype { get; private set; }
        public string? Subtype { get; private set; }
        public virtual CardSet Set { get; private set; }
        public Rarity Rarity { get; private set; }
        public bool IsLegendary { get; private set; }
        public int ManaValue { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTime PriceLastUpdated { get; private set; }
        public int? Power { get; private set; }
        public int? Toughness { get; private set; }

        public void UpdateCardPrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new InvalidOperationException("Card price cannot be negative.");

            Price = newPrice;
            PriceLastUpdated = DateTime.UtcNow;
        }
    }
}