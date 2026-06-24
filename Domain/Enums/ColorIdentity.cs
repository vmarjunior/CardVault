using System;

namespace CardVault.Domain.Enums
{
    [Flags]
    public enum ColorIdentity
    {
        // 0. No color (Wastes/Generic)
        Colorless = 0,

        // 1. Monocolor
        White = 1 << 0,  // 1
        Blue = 1 << 1,  // 2
        Black = 1 << 2,  // 4
        Red = 1 << 3,  // 8
        Green = 1 << 4,  // 16

        // 2. Guilds of Ravnica (Bicolor)
        // Aliadas
        Azorius = White | Blue,
        Dimir = Blue | Black,
        Rakdos = Black | Red,
        Gruul = Red | Green,
        Selesnya = Green | White,
        // Inimigas
        Orzhov = White | Black,
        Izzet = Blue | Red,
        Golgari = Black | Green,
        Boros = Red | White,
        Simic = Blue | Green,

        // 3. Shards of Alara (Tricolor - Ally)
        Bant = White | Blue | Green,
        Esper = White | Blue | Black,
        Grixis = Blue | Black | Red,
        Jund = Black | Red | Green,
        Naya = White | Red | Green,

        // 4. Clans of Tarkir / Wedges (Tricolor - Enemy)
        Abzan = White | Black | Green,
        Jeskai = White | Blue | Red,
        Sultai = Blue | Black | Green,
        Mardu = White | Black | Red,
        Temur = Blue | Red | Green,

        // 5. Quad Color (Informal names based on Nephilim / Commander)
        YoreTiller = White | Blue | Black | Red,   // Non-G
        GlintEye = Blue | Black | Red | Green,   // Non-W
        DuneBrood = White | Black | Red | Green,  // Non-U
        InkTreader = White | Blue | Red | Green,   // Non-B
        WitchMaw = White | Blue | Black | Green, // Non-R

        // 6. 5C/5-Color (WUBRG)
        Wubrg = White | Blue | Black | Red | Green
    }
}