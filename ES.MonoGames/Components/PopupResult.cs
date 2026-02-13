namespace Components;

public class PopupResult
{
    public bool Confirmed { get; init; }
    public string? TextValue { get; init; }
    public int SelectedIndex { get; init; } = -1;
    public string? SelectedItem { get; init; }
    public object? Value { get; init; }
}
