namespace RpgTkoolMvSaveEditor.Application;

public class Switch(int id, string name, bool? value)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public bool? Value { get; } = value;
}