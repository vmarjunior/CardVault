using CardVault.Domain.Entities;
using CardVault.Domain.Enums;
using CardVault.Domain.Repositories;
using CardVault.Infrastructure.ExternalServices;
using FluentAssertions;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace CardVault.UnitTests.ExternalServices
{
    public class ScryfallServiceTests
    {
        private readonly Mock<ICardSetRepository> _cardSetRepository = new();

        [Fact]
        public async Task GetCardAsync_WhenNameIsEmpty_ReturnsNull()
        {
            var sut = CreateService(_ =>
                throw new Exception("HTTP should not be called"));

            var result = await sut.GetCardAsync("", null);

            result.Should().BeNull();

            _cardSetRepository.Verify(
                x => x.GetByCodeAsync(It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public async Task GetCardAsync_WhenCardDoesNotExist_ReturnsNull()
        {
            var sut = CreateService(_ =>
                new HttpResponseMessage(HttpStatusCode.NotFound));

            var result = await sut.GetCardAsync("Unknown Card", null);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCardAsync_WhenSetExists_DoesNotCreateNewSet()
        {
            var existingSet = CreateSet();

            _cardSetRepository
                .Setup(x => x.GetByCodeAsync("m11"))
                .ReturnsAsync(existingSet);

            var sut = CreateService(_ => Json(CardJson()));

            var result = await sut.GetCardAsync("Lightning Bolt", null);

            result.Should().NotBeNull();
            result!.Set.Should().Be(existingSet);

            _cardSetRepository.Verify(
                x => x.AddAsync(It.IsAny<CardSet>()),
                Times.Never);
        }

        [Fact]
        public async Task GetCardAsync_WhenSetDoesNotExist_CreatesNewSet()
        {
            _cardSetRepository
                .Setup(x => x.GetByCodeAsync("m11"))
                .ReturnsAsync((CardSet?)null);

            var sut = CreateService(request =>
            {
                if (request.RequestUri!.AbsoluteUri.Contains("/sets/"))
                    return Json(SetJson());

                return Json(CardJson());
            });

            var result = await sut.GetCardAsync("Lightning Bolt", null);

            result.Should().NotBeNull();

            _cardSetRepository.Verify(
                x => x.AddAsync(It.IsAny<CardSet>()),
                Times.Once);
        }

        [Fact]
        public async Task GetCardAsync_WhenSetLookupFails_FallsBackToNameLookup()
        {
            _cardSetRepository
                .Setup(x => x.GetByCodeAsync("m11"))
                .ReturnsAsync(CreateSet());

            var calls = 0;

            var sut = CreateService(request =>
            {
                calls++;

                var url = request.RequestUri!.AbsoluteUri;

                if (url.Contains("set=m11"))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Json(CardJson());
            });

            var result = await sut.GetCardAsync("Lightning Bolt", "m11");

            result.Should().NotBeNull();
            calls.Should().Be(2);
        }

        [Fact]
        public async Task GetCardAsync_MapsCardPropertiesCorrectly()
        {
            var existingSet = CreateSet();

            _cardSetRepository
                .Setup(x => x.GetByCodeAsync("m11"))
                .ReturnsAsync(existingSet);

            var sut = CreateService(_ => Json(CardJson()));

            var result = await sut.GetCardAsync("Lightning Bolt", null);

            result.Should().NotBeNull();

            result!.Name.Should().Be("Lightning Bolt");
            result.Artist.Should().Be("Christopher Moeller");
            result.Rarity.Should().Be(Rarity.Common);
            result.ManaValue.Should().Be(1);
            result.Price.Should().Be(2.50m);
            result.ColorIdentity.Should().Be("R");
            result.IsLegendary.Should().BeFalse();
        }

        private ScryfallService CreateService(
            Func<HttpRequestMessage, HttpResponseMessage> handler)
        {
            var client = new HttpClient(new FakeHttpMessageHandler(handler));

            return new ScryfallService(
                client,
                _cardSetRepository.Object);
        }

        private static CardSet CreateSet()
        {
            return new CardSet(
                "m11",
                "Magic 2011",
                "https://example.com/set-image.svg",
                new DateTime(2010, 7, 16),
                SetType.Core,
                249);
        }

        private static HttpResponseMessage Json(object body)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(body)
            };
        }

        private static object CardJson()
        {
            return new
            {
                name = "Lightning Bolt",
                image_uris = new
                {
                    normal = "image.png"
                },
                artist = "Christopher Moeller",
                color_identity = new[] { "R" },
                type_line = "Instant",
                rarity = "common",
                cmc = 1,
                oracle_text = "Deal 3 damage.",
                prices = new
                {
                    usd = "2.50"
                },
                power = (string?)null,
                toughness = (string?)null,
                set = "m11"
            };
        }

        private static object SetJson()
        {
            return new
            {
                code = "m11",
                name = "Magic 2011",
                icon_svg_uri = "https://example.com/m11.svg",
                released_at = "2010-07-16",
                set_type = "core",
                card_count = 249
            };
        }
    }
}