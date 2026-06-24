namespace CardVault.Application.Interfaces
{
    public interface IPasswordHasher
    {
        public (byte[] passwordHash, byte[] passwordSalt) HashPassword(string plainPassword);
        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
