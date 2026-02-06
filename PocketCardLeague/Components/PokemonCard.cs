using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PocketCardLeague.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCardLeague.Components;

public class PokemonCard : Card
{
    public int Level { get; set; } = 1;
    public int InnatePower { get; set; } = 0;
    public bool Shiny { get; set; } = false;
    public bool Gigantamax { get; set; } = false;
    public int DexId { get; set; } = 0;
    public int FormId { get; set; } = 0;
    public int VariantId { get; set; } = 0;
    public PokemonType Type1 { get; set; } = PokemonType.UNK;
    public PokemonType? Type2 { get; set; } = null;
    public string SpriteIdentifier => $"{DexId:D4}_{FormId:D3}_mf_{(Gigantamax ? 'g' : 'n')}_{VariantId:D8}_{(Shiny ? 'r' : 'n')}";

    public List<BerryType> Cost { get; set; } = [BerryType.Green, BerryType.Green, BerryType.Void];

    public List<PokemonType> Types => Type2 == null ? [Type1] : [Type1, Type2.Value];

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
