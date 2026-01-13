namespace Config;

public abstract class BaseConfig
{
    public string FontsRoot { get; set; } = string.Empty;
    public string TexturesRoot { get; set; } = string.Empty;
    public string EffectsRoot { get; set; } = string.Empty;
    public string TracksRoot { get; set; } = string.Empty;
}
