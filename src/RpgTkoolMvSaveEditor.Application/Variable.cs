namespace RpgTkoolMvSaveEditor.Application;

public class Variable(int id, string name, object? value)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public object? Value { get; } = value;
}