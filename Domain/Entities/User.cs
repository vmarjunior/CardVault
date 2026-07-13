using System;
using System.Collections.Generic;

namespace CardVault.Domain.Entities
{
    public class User
    {
        private User() { }

        public User(string accountName, byte[] passwordHash, byte[] passwordSalt, string nickname)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(accountName);
            ArgumentNullException.ThrowIfNull(passwordHash);
            ArgumentNullException.ThrowIfNull(passwordSalt);
            ArgumentException.ThrowIfNullOrWhiteSpace(nickname);

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
            ArgumentException.ThrowIfNullOrWhiteSpace(accountName);
            ArgumentNullException.ThrowIfNull(passwordHash);
            ArgumentNullException.ThrowIfNull(passwordSalt);

            AccountName = accountName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void UpdateProfile(string nickname)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nickname);

            Nickname = nickname;
        }

        public void UpdateLastTimeActive()
        {
            LastActive = DateTime.UtcNow;
        }
    }
}