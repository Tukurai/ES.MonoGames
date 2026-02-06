using PocketCardLeague.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCardLeague.Helpers;

public static class CollectionManager
{
    public static Deck? EditingDeck { get; set; }
    public static Deck? ActiveDeck { get; set; }
    public static List<Deck> Decks { get; set; } = [];
    public static List<PokemonCard> PokemonCards { get; set; } = [];
    public static List<BerryCard> BerryCards { get; set; } = [];
}
