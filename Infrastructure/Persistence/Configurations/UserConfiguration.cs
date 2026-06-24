using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardVault.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.AccountName)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasIndex(u => u.AccountName)
                   .IsUnique();

            builder.Property(u => u.Nickname)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.PasswordSalt)
                   .IsRequired();

            builder.Property(u => u.Created)
                   .IsRequired();

            builder.Property(u => u.LastActive)
                   .IsRequired(false);

            builder.Navigation(u => u.UserCards)
                   .HasField("_userCards")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(u => u.Decks)
                   .HasField("_decks")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}