using CardVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CardVault.Infrastructure.Persistence.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(c => c.ImageUrl)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(c => c.Artist)
                   .HasMaxLength(100);

            builder.Property(c => c.Supertype)
                   .HasMaxLength(50);

            builder.Property(c => c.Subtype)
                   .HasMaxLength(50);

            builder.Property(c => c.Description)
                   .HasMaxLength(1000);

            builder.Property(c => c.Price)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(c => c.PriceLastUpdated)
                   .IsRequired();

            builder.Property(c => c.ManaValue)
                   .IsRequired();

            builder.Property(c => c.Power)
                   .IsRequired(false);

            builder.Property(c => c.Toughness)
                   .IsRequired(false);

            builder.Property(c => c.Rarity)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(c => c.ColorIdentity)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property<Guid>("SetId").IsRequired();
        }
    }
}