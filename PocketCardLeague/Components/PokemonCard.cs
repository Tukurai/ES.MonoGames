using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Consts;
using PocketCardLeague.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PocketCardLeague.Components;

public class PokemonCard : Card
{
    public int Level { get; set; } = 1;
    public int InnatePower { get; set; } = 0;
    public string? Nickname { get; set; } = null;
    public PokeDexEntry? BasePokemon { get; set; } = null;

    public List<BerryEnergyType> Cost { get; set; } = [BerryEnergyType.Green, BerryEnergyType.Green, BerryEnergyType.Void];

    [JsonIgnore]
    public List<PokemonType> Types => BasePokemon is null ? [] : BasePokemon.Types;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
