using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CardVault.Domain.Entities;
using CardVault.Infrastructure.Seed;

namespace CardVault.Infrastructure
{
    public sealed class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardSet> CardSets { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Only production data seeding, no test data seeding
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            SeedCardSets.Seed(modelBuilder);
            SeedCards.Seed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));

            base.OnConfiguring(optionsBuilder);
        }
    }
}