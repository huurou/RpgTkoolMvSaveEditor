namespace RpgTkoolMvSaveEditor.Application;

public class GameSwitch
{
    public string Id { get; }
    public string Name { get; }
    public bool? Value { get; }

    public GameSwitch(string id, string name, bool? value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}