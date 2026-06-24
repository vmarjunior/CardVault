using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardVault.Infrastructure.Persistence.Configurations
{
    public class DeckConfiguration : IEntityTypeConfiguration<Deck>
    {
        public void Configure(EntityTypeBuilder<Deck> builder)
        {
            builder.ToTable("Decks");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(d => d.Type)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(d => d.IsPrivate)
                   .IsRequired();

            builder.Navigation(d => d.UserCards)
                   .HasField("_userCards")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<Guid>("UserId");

            builder.HasOne(d => d.User)
                   .WithMany(u => u.Decks)
                   .HasForeignKey("UserId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}