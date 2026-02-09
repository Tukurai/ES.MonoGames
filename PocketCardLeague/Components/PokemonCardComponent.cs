using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Enums;
using System.Collections.Generic;

namespace PocketCardLeague.Components;

public class PokemonCardComponent : CardComponent
{
    public PokeCard? Card { get; set; } = null;

    public List<BerryEnergyType> Cost { get; set; } = [BerryEnergyType.Green, BerryEnergyType.Green, BerryEnergyType.Void];

    public List<PokemonType> Types => Card is null ? [] : Card.BasePokemon.Types;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
