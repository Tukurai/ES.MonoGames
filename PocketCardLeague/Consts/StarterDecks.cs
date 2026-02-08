using PocketCardLeague.Components;
using PocketCardLeague.Enums;

namespace PocketCardLeague.Consts;

public static class StarterDecks
{
    public static Deck Fire => new("Fire Starter")
    {
        MainDeck =
        [
            new PokemonCard { CardName = "Charmander", Level = 5, Type1 = PokemonType.Fire, DexId = 4, Cost = [BerryEnergyType.Red, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Vulpix", Level = 5, Type1 = PokemonType.Fire, DexId = 37, Cost = [BerryEnergyType.Red, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Growlithe", Level = 8, Type1 = PokemonType.Fire, DexId = 58, Cost = [BerryEnergyType.Red, BerryEnergyType.Red, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Ponyta", Level = 6, Type1 = PokemonType.Fire, DexId = 77, Cost = [BerryEnergyType.Red, BerryEnergyType.Void] },
        ],
        SideDeck =
        [
            new BerryCard { CardName = "Rawst Berry", BerryTypes = [BerryEnergyType.Red] },
            new BerryCard { CardName = "Rawst Berry", BerryTypes = [BerryEnergyType.Red] },
        ],
        FaceCard = new PokemonCard { CardName = "Charmander", Level = 5, Type1 = PokemonType.Fire, DexId = 4 },
    };

    public static Deck Water => new("Water Starter")
    {
        MainDeck =
        [
            new PokemonCard { CardName = "Squirtle", Level = 5, Type1 = PokemonType.Water, DexId = 7, Cost = [BerryEnergyType.Blue, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Psyduck", Level = 5, Type1 = PokemonType.Water, DexId = 54, Cost = [BerryEnergyType.Blue, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Poliwag", Level = 8, Type1 = PokemonType.Water, DexId = 60, Cost = [BerryEnergyType.Blue, BerryEnergyType.Blue, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Staryu", Level = 6, Type1 = PokemonType.Water, DexId = 120, Cost = [BerryEnergyType.Blue, BerryEnergyType.Void] },
        ],
        SideDeck =
        [
            new BerryCard { CardName = "Oran Berry", BerryTypes = [BerryEnergyType.Blue] },
            new BerryCard { CardName = "Oran Berry", BerryTypes = [BerryEnergyType.Blue] },
        ],
        FaceCard = new PokemonCard { CardName = "Squirtle", Level = 5, Type1 = PokemonType.Water, DexId = 7 },
    };

    public static Deck Grass => new("Grass Starter")
    {
        MainDeck =
        [
            new PokemonCard { CardName = "Bulbasaur", Level = 5, Type1 = PokemonType.Grass, Type2 = PokemonType.Poison, DexId = 1, Cost = [BerryEnergyType.Green, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Oddish", Level = 5, Type1 = PokemonType.Grass, Type2 = PokemonType.Poison, DexId = 43, Cost = [BerryEnergyType.Green, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Bellsprout", Level = 8, Type1 = PokemonType.Grass, Type2 = PokemonType.Poison, DexId = 69, Cost = [BerryEnergyType.Green, BerryEnergyType.Green, BerryEnergyType.Void] },
            new PokemonCard { CardName = "Tangela", Level = 6, Type1 = PokemonType.Grass, DexId = 114, Cost = [BerryEnergyType.Green, BerryEnergyType.Void] },
        ],
        SideDeck =
        [
            new BerryCard { CardName = "Sitrus Berry", BerryTypes = [BerryEnergyType.Green] },
            new BerryCard { CardName = "Sitrus Berry", BerryTypes = [BerryEnergyType.Green] },
        ],
        FaceCard = new PokemonCard { CardName = "Bulbasaur", Level = 5, Type1 = PokemonType.Grass, Type2 = PokemonType.Poison, DexId = 1 },
    };
}
