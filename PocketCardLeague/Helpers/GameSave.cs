using PocketCardLeague.Components;
using PocketCardLeague.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCardLeague.Helpers;

public class GameSave
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CharacterName { get; set; } = string.Empty;
    public int Money { get; set; } = 0;
    public int RouteId { get; set; } = 0;
    public int RouteStep { get; set; } = 0;
    public GameType GameType { get; set; } = GameType.Normal;
    public List<BadgeType> Badges { get; set; } = [];
    public List<EventType> AvailableEvents { get; set; } = [];
    public Deck? BattleOpponent { get; set; } = null;
    public Deck? EditingDeck { get; set; }
    public Deck? ActiveDeck { get; set; }
    public List<Deck> Decks { get; set; } = [];
    public List<PokemonCardComponent> PokemonCards { get; set; } = [];
    public List<BerryCardComponent> BerryCards { get; set; } = [];
}
