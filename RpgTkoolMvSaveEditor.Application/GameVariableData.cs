namespace RpgTkoolMvSaveEditor.Application;

public class GameVariable
{
    public string Id { get; }
    public string Name { get; }
    public object? Value { get; }

    public GameVariable(string id, string name, object? value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}
