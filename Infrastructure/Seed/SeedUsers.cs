using CardVault.Domain.Entities;
using CardVault.Infrastructure.Helpers;
using System.Text.Json;

namespace CardVault.Infrastructure.Seed
{
    public static class SeedUsers
    {
        private class UserSeedDto
        {
            public Guid Id { get; set; }
            public string AccountName { get; set; } = default!;
            public string Password { get; set; } = default!;
            public string Nickname { get; set; } = default!;
            public DateTime Created { get; set; }
            public DateTime? LastActive { get; set; }
        }

        public static void Seed(AppDbContext context)
        {
            if (context.Users.Any()) return;

            var userData = File.ReadAllText("../Infrastructure/Seed/Data/UserSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var usersDto = JsonSerializer.Deserialize<List<UserSeedDto>>(userData, options);

            if (usersDto == null || !usersDto.Any()) return;

            var passwordHasher = new PasswordHasher();

            foreach (var dto in usersDto)
            {
                var (passwordHash, passwordSalt) = passwordHasher.HashPassword(dto.Password);

                var user = new User(
                    dto.AccountName,
                    passwordHash,
                    passwordSalt,
                    dto.Nickname
                );

                context.Entry(user).Property(u => u.Id).CurrentValue = dto.Id;

                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}