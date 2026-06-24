using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardVault.Infrastructure.Persistence.Configurations
{
    public class UserCardConfiguration : IEntityTypeConfiguration<UserCard>
    {
        public void Configure(EntityTypeBuilder<UserCard> builder)
        {
            builder.ToTable("UserCards");

            builder.HasKey(uc => uc.Id);

            builder.Property<Guid>("UserId");

            builder.HasOne(uc => uc.User)
                   .WithMany(u => u.UserCards)
                   .HasForeignKey("UserId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property<Guid>("CardId");

            builder.HasOne(uc => uc.Card)
                   .WithMany()
                   .HasForeignKey("CardId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property<Guid?>("DeckId");

            builder.HasOne(uc => uc.Deck)
                   .WithMany(d => d.UserCards)
                   .HasForeignKey("DeckId")
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(uc => uc.IsVirtual)
                   .IsRequired();
        }
    }
}