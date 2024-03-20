namespace RpgTkoolMvSaveEditor.Application;

public class Weapon(int id, string name, string description, int count)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public int Count { get; } = count;
}