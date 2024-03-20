namespace RpgTkoolMvSaveEditor.Application;

public class GameVariable(string id, string name, object? value)
{
    public string Id { get; } = id;
    public string Name { get; } = name;
    public object? Value { get; } = value;
}