using CardVault.Application.Interfaces;
using CardVault.Domain.Entities;
using CardVault.Domain.Enums;
using CardVault.Domain.Repositories;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CardVault.Infrastructure.ExternalServices
{
    public class ScryfallService(HttpClient httpClient, ICardSetRepository cardSetRepository) : ICardExternalService
    {
        public async Task<Card?> GetCardAsync(string name, string? setCode)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var baseUrl = $"https://api.scryfall.com/cards/named?exact={Uri.EscapeDataString(name)}";
            ScryfallCardResponse? cardDto = null;

            if (!string.IsNullOrWhiteSpace(setCode))
            {
                var setUrl = $"{baseUrl}&set={Uri.EscapeDataString(setCode)}";
                cardDto = await FetchCardDtoAsync(setUrl);
            }

            cardDto ??= await FetchCardDtoAsync(baseUrl);

            if (cardDto == null)
                return null;

            var cardSet = await GetOrCreateSetAsync(cardDto.SetCode)
                ?? throw new InvalidOperationException($"Failed to retrieve set data for set code: {cardDto.SetCode}");

            return MapToDomain(cardDto, cardSet);
        }

        private async Task<ScryfallCardResponse?> FetchCardDtoAsync(string url)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<ScryfallCardResponse>(url);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        private async Task<CardSet?> GetOrCreateSetAsync(string setCode)
        {
            var existingSet = await cardSetRepository.GetByCodeAsync(setCode);
            if (existingSet != null) return existingSet;

            var setUrl = $"https://api.scryfall.com/sets/{setCode}";
            var setDto = await httpClient.GetFromJsonAsync<ScryfallSetResponse>(setUrl);

            if (setDto == null) return null;

            DateTime.TryParse(setDto.ReleasedAt, out var releaseDate);

            var setType = MapSetType(setDto.SetType.Trim());

            var newSet = new CardSet(
                code: setDto.Code.Trim(),
                name: setDto.Name.Trim(),
                imageUrl: setDto.IconSvgUri?.Trim() ?? string.Empty,
                releaseDate: releaseDate == default ? null : releaseDate,
                setType: setType,
                cardsCount: setDto.CardCount
            );

            await cardSetRepository.AddAsync(newSet);

            return newSet;
        }

        private SetType MapSetType(string scryfallSetType)
        {
            if (string.IsNullOrWhiteSpace(scryfallSetType))
                return SetType.Other;

            return scryfallSetType.ToLower() switch
            {
                "expansion" => SetType.Expansion,
                "masters" => SetType.Masters,
                "commander" => SetType.Commander,
                "core" => SetType.Core,
                "secret_lair" or "drop" => SetType.SecretLair,
                _ => SetType.Other
            };
        }

        private Card MapToDomain(ScryfallCardResponse dto, CardSet cardSet)
        {
            var types = dto.TypeLine.Split(" — ");
            var supertype = types[0];
            var subtype = types.Length > 1 ? types[1] : null;
            var isLegendary = supertype.Contains("Legendary");

            decimal price = decimal.TryParse(dto.Prices?.Usd?.Replace(".", ","), out var p) ? p : 0m;
            int? power = int.TryParse(dto.Power, out var pow) ? pow : null;
            int? toughness = int.TryParse(dto.Toughness, out var tou) ? tou : null;
            var rarity = Enum.Parse<Rarity>(dto.Rarity, ignoreCase: true);
            var colorIdentity = string.Join("", dto.ColorIdentity);

            return new Card(
                name: dto.Name,
                imageUrl: dto.ImageUris?.Normal ?? string.Empty,
                artist: dto.Artist,
                colorIdentity: colorIdentity,
                supertype: supertype,
                subtype: subtype,
                set: cardSet,
                rarity: rarity,
                isLegendary: isLegendary,
                manaValue: (int)dto.Cmc,
                description: dto.OracleText,
                price: price,
                power: power,
                toughness: toughness
            );
        }

        private class ScryfallCardResponse
        {
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("image_uris")] public ScryfallImageUris? ImageUris { get; set; }
            [JsonPropertyName("artist")] public string Artist { get; set; }
            [JsonPropertyName("color_identity")] public string[] ColorIdentity { get; set; }
            [JsonPropertyName("type_line")] public string TypeLine { get; set; }
            [JsonPropertyName("rarity")] public string Rarity { get; set; }
            [JsonPropertyName("cmc")] public decimal Cmc { get; set; }
            [JsonPropertyName("oracle_text")] public string OracleText { get; set; }
            [JsonPropertyName("prices")] public ScryfallPrices? Prices { get; set; }
            [JsonPropertyName("power")] public string? Power { get; set; }
            [JsonPropertyName("toughness")] public string? Toughness { get; set; }
            [JsonPropertyName("set")] public string SetCode { get; set; }
        }

        private class ScryfallSetResponse
        {
            [JsonPropertyName("code")] public string Code { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("icon_svg_uri")] public string? IconSvgUri { get; set; }
            [JsonPropertyName("released_at")] public string? ReleasedAt { get; set; }
            [JsonPropertyName("set_type")] public string SetType { get; set; }
            [JsonPropertyName("card_count")] public int CardCount { get; set; }
        }

        private class ScryfallImageUris
        {
            [JsonPropertyName("normal")] public string Normal { get; set; }
        }

        private class ScryfallPrices
        {
            [JsonPropertyName("usd")] public string? Usd { get; set; }
            [JsonPropertyName("usd_foil")] public string? UsdFoil { get; set; }
        }
    }
}