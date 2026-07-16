using CardVault.Application.Interfaces;
using CardVault.Application.Queries;
using CardVault.Application.Services;
using CardVault.Domain.Entities;
using CardVault.Domain.Enums;
using CardVault.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CardVault.UnitTests.Cards
{
    public class CardServiceTests
    {
        private readonly Mock<ICardRepository> _cardRepository;
        private readonly Mock<ICardQueries> _cardQueries;
        private readonly Mock<ICardExternalService> _cardExternalService;

        private readonly CardService _service;

        public CardServiceTests()
        {
            _cardRepository = new Mock<ICardRepository>();
            _cardQueries = new Mock<ICardQueries>();
            _cardExternalService = new Mock<ICardExternalService>();

            _service = new CardService(
                _cardRepository.Object,
                _cardQueries.Object,
                _cardExternalService.Object
            );
        }

        [Fact]
        public async Task GetOrCreateAsync_ShouldReturnExistingCard_WhenCardExists()
        {
            // Arrange
            var existingCard = CreateCard();

            _cardRepository
                .Setup(x => x.GetByNameAndSetAsync(
                    "Black Lotus",
                    "LEA"))
                .ReturnsAsync(existingCard);

            // Act
            var result = await _service.GetOrCreateAsync(
                "Black Lotus",
                "LEA");

            // Assert
            result.Should().Be(existingCard);

            _cardExternalService.Verify(
                x => x.GetCardAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);

            _cardRepository.Verify(
                x => x.AddAsync(
                    It.IsAny<Card>()),
                Times.Never);
        }

        [Fact]
        public async Task GetOrCreateAsync_ShouldSaveCard_WhenCardExistsExternally()
        {
            // Arrange
            var externalCard = CreateCard();

            _cardRepository
                .Setup(x => x.GetByNameAndSetAsync(
                    "Black Lotus",
                    "LEA"))
                .ReturnsAsync((Card?)null);

            _cardExternalService
                .Setup(x => x.GetCardAsync(
                    "Black Lotus",
                    "LEA"))
                .ReturnsAsync(externalCard);

            // Act
            var result = await _service.GetOrCreateAsync(
                "Black Lotus",
                "LEA");

            // Assert
            result.Should().Be(externalCard);

            _cardRepository.Verify(
                x => x.AddAsync(externalCard),
                Times.Once);
        }

        [Fact]
        public async Task GetOrCreateAsync_ShouldThrow_WhenCardCannotBeFound()
        {
            // Arrange
            _cardRepository
                .Setup(x => x.GetByNameAndSetAsync(
                    "Fake Card",
                    "XYZ"))
                .ReturnsAsync((Card?)null);

            _cardExternalService
                .Setup(x => x.GetCardAsync(
                    "Fake Card",
                    "XYZ"))
                .ReturnsAsync((Card?)null);

            // Act
            Func<Task> action = async () =>
                await _service.GetOrCreateAsync(
                    "Fake Card",
                    "XYZ");

            // Assert
            await action.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Card could not be found.");

            _cardRepository.Verify(
                x => x.AddAsync(It.IsAny<Card>()),
                Times.Never);
        }


        private static Card CreateCard()
        {
            var cardSet = new CardSet(
                code: "LEA",
                name: "Limited Edition Alpha",
                imageUrl: "https://example.com/set.png",
                releaseDate: new DateTime(1993, 8, 5),
                setType: SetType.Core,
                cardsCount: 295
            );

            return new Card(
                name: "Black Lotus",
                imageUrl: "https://example.com/card.png",
                artist: "Christopher Rush",
                colorIdentity: "C",
                supertype: "",
                subtype: "",
                set: cardSet,
                rarity: Rarity.Rare,
                isLegendary: false,
                manaValue: 0,
                description: "Add three mana of any one color.",
                price: 10000m,
                power: null,
                toughness: null
            );
        }
    }
}