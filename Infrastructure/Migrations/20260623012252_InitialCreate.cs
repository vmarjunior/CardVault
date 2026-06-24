using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardVault.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SetType = table.Column<int>(type: "integer", nullable: false),
                    CardsCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Nickname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastActive = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Artist = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ColorIdentity = table.Column<int>(type: "integer", nullable: false),
                    Supertype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Subtype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rarity = table.Column<int>(type: "integer", nullable: false),
                    IsLegendary = table.Column<bool>(type: "boolean", nullable: false),
                    ManaValue = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    PriceLastUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Power = table.Column<int>(type: "integer", nullable: true),
                    Toughness = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_CardSets_SetId",
                        column: x => x.SetId,
                        principalTable: "CardSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeckId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsVirtual = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCards_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CardSets",
                columns: new[] { "Id", "CardsCount", "Code", "ImageUrl", "Name", "ReleaseDate", "SetType" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000001"), 295, "LEA", "https://svgs.scryfall.io/sets/lea.svg", "Limited Edition Alpha", new DateTime(1993, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000002"), 92, "ARN", "https://svgs.scryfall.io/sets/arn.svg", "Arabian Nights", new DateTime(1993, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000003"), 306, "MRD", "https://svgs.scryfall.io/sets/mrd.svg", "Mirrodin", new DateTime(2003, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000004"), 306, "RAV", "https://svgs.scryfall.io/sets/rav.svg", "Ravnica: City of Guilds", new DateTime(2005, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000005"), 249, "ZEN", "https://svgs.scryfall.io/sets/zen.svg", "Zendikar", new DateTime(2009, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000006"), 264, "ISD", "https://svgs.scryfall.io/sets/isd.svg", "Innistrad", new DateTime(2011, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000007"), 274, "RTR", "https://svgs.scryfall.io/sets/rtr.svg", "Return to Ravnica", new DateTime(2012, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000008"), 269, "KTK", "https://svgs.scryfall.io/sets/ktk.svg", "Khans of Tarkir", new DateTime(2014, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000009"), 269, "DOM", "https://svgs.scryfall.io/sets/dom.svg", "Dominaria", new DateTime(2018, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000010"), 264, "WAR", "https://svgs.scryfall.io/sets/war.svg", "War of the Spark", new DateTime(2019, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000011"), 269, "ELD", "https://svgs.scryfall.io/sets/eld.svg", "Throne of Eldraine", new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000012"), 254, "MH1", "https://svgs.scryfall.io/sets/mh1.svg", "Modern Horizons", new DateTime(2019, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000013"), 303, "MH2", "https://svgs.scryfall.io/sets/mh2.svg", "Modern Horizons 2", new DateTime(2021, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000014"), 302, "NEO", "https://svgs.scryfall.io/sets/neo.svg", "Kamigawa: Neon Dynasty", new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000015"), 281, "LTR", "https://svgs.scryfall.io/sets/ltr.svg", "The Lord of the Rings: Tales of Middle-earth", new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Artist", "ColorIdentity", "Description", "ImageUrl", "IsLegendary", "ManaValue", "Name", "Power", "Price", "PriceLastUpdated", "Rarity", "SetId", "Subtype", "Supertype", "Toughness" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000001"), "Christopher Rush", 0, "{T}, Sacrifice Black Lotus: Add three mana of any one color.", "https://cards.scryfall.io/large/front/b/d/bd8fa327-dd41-4737-8f19-2cf5eb1f7cdd.jpg", false, 0, "Black Lotus", null, 500000.00m, new DateTime(2026, 6, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("a1b2c3d4-0000-0000-0000-000000000001"), null, "Artifact", null },
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000002"), "Matt Stewart", 2, "At the beginning of your upkeep, look at the top card of your library. You may reveal that card. If an instant or sorcery card is revealed this way, transform Delver of Secrets.", "https://cards.scryfall.io/large/front/1/1/11bf83bb-c95b-4b4f-9a56-ce7a1816307a.jpg", false, 1, "Delver of Secrets // Insectile Aberration", 1, 1.50m, new DateTime(2026, 6, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("a1b2c3d4-0000-0000-0000-000000000006"), "Human Wizard", "Creature", 1 },
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000003"), "Simon Dominic", 4, "Whenever Ragavan, Nimble Pilferer deals combat damage to a player, create a Treasure token and exile the top card of that player's library. Until end of turn, you may cast that card.", "https://cards.scryfall.io/large/front/a/9/a9738cda-adb1-47fb-9f4c-ecd930228c4d.jpg", true, 1, "Ragavan, Nimble Pilferer", 2, 45.00m, new DateTime(2026, 6, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), 4, new Guid("a1b2c3d4-0000-0000-0000-000000000013"), "Monkey Pirate", "Legendary Creature", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_SetId",
                table: "Cards",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_CardSets_Code",
                table: "CardSets",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decks_UserId",
                table: "Decks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_CardId",
                table: "UserCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_DeckId",
                table: "UserCards",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_UserId",
                table: "UserCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountName",
                table: "Users",
                column: "AccountName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCards");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "CardSets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
