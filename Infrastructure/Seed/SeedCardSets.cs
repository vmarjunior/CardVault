using CardVault.Domain.Entities;
using CardVault.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CardVault.Infrastructure.Seed
{
    public static class SeedCardSets
    {
        private class CardSetSeedDto
        {
            public Guid Id { get; set; }
            public string Code { get; set; } = default!;
            public string Name { get; set; } = default!;
            public string ImageUrl { get; set; } = default!;
            public DateTime? ReleaseDate { get; set; }
            public int SetType { get; set; }
            public int CardsCount { get; set; }
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            var setData = File.ReadAllText("../Infrastructure/Seed/Data/CardSetSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var setsDto = JsonSerializer.Deserialize<List<CardSetSeedDto>>(setData, options);

            if (setsDto == null || !setsDto.Any()) return;

            var setsToSeed = setsDto.Select(dto => new
            {
                Id = dto.Id,
                Code = dto.Code,
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
                ReleaseDate = dto.ReleaseDate,
                SetType = (SetType)dto.SetType,
                CardsCount = dto.CardsCount
            });

            modelBuilder.Entity<CardSet>().HasData(setsToSeed);
        }
    }
}