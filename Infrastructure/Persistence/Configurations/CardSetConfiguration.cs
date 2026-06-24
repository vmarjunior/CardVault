using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardVault.Infrastructure.Persistence.Configurations
{
    public class CardSetConfiguration : IEntityTypeConfiguration<CardSet>
    {
        public void Configure(EntityTypeBuilder<CardSet> builder)
        {
            builder.ToTable("CardSets");

            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.Code)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.HasIndex(cs => cs.Code)
                   .IsUnique();

            builder.Property(cs => cs.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(c => c.ImageUrl)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(cs => cs.ReleaseDate)
                   .IsRequired(false);

            builder.Property(cs => cs.CardsCount)
                   .IsRequired();

            builder.Property(cs => cs.SetType)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Navigation(cs => cs.Cards)
                   .HasField("_cards")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(cs => cs.Cards)
                   .WithOne(c => c.Set)
                   .HasForeignKey("SetId")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}