namespace RpgTkoolMvSaveEditor.Application;

public class Variable
{
    public int Id { get; }
    public string Name { get; }
    public object? Value { get; }

    public Variable(int id, string name, object? value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}