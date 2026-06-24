using System;
using System.Collections.Generic;

namespace CardVault.Domain.Entities
{
    public class User
    {
        private User() { }

        public User(string accountName, byte[] passwordHash, byte[] passwordSalt, string nickname)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                throw new InvalidOperationException("Account name cannot be empty.");
            if (passwordHash == null || passwordSalt == null)
                throw new InvalidOperationException("User password cannot be empty.");
            if (string.IsNullOrWhiteSpace(nickname))
                throw new InvalidOperationException("User name cannot be empty.");

            Id = Guid.NewGuid();
            AccountName = accountName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Nickname = nickname;
            Created = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string AccountName { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string Nickname { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? LastActive { get; private set; }

        private readonly List<UserCard> _userCards = new();
        private readonly List<Deck> _decks = new();

        public virtual IReadOnlyCollection<UserCard> UserCards => _userCards.AsReadOnly();
        public virtual IReadOnlyCollection<Deck> Decks => _decks.AsReadOnly();

        public void UpdateAccount(string accountName, byte[] passwordHash, byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                throw new InvalidOperationException("Account name cannot be empty.");
            if (passwordHash == null || passwordSalt == null)
                throw new ArgumentException("PasswordHash or PasswordSalt can't be empty or null.");

            AccountName = accountName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void UpdateProfile(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
                throw new InvalidOperationException("Nickname cannot be empty or null.");

            Nickname = nickname;
        }

        public void UpdateLastTimeActive()
        {
            LastActive = DateTime.UtcNow;
        }
    }
}