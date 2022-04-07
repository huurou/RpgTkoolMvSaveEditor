namespace RpgTkoolMvSaveEditor.Application;

public class GameVariable
{
    public string Id { get; }
    public string Name { get; }
    public object? Value { get; }

    public GameVariable(int id, string name, object? value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}