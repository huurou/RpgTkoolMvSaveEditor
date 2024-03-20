namespace RpgTkoolMvSaveEditor.Application;

public class GameSwitch(string id, string name, bool? value)
{
    public string Id { get; } = id;
    public string Name { get; } = name;
    public bool? Value { get; } = value;
}