namespace RpgTkoolMvSaveEditor.Application;

public class Variable
{
    public string Id { get; }
    public string Name { get; }
    public object? Value { get; }

    public Variable(string id, string name, object? value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}