using Config;

namespace PocketCardLeague.Config;

public class PocketCardLeagueConfig : BaseConfig
{
    public PocketCardLeagueConfig()
    {
        FontsRoot = "fonts/";
        TexturesRoot = "spritesheets/";
        EffectsRoot = "effects/";
        TracksRoot = "tracks/";
    }
}
