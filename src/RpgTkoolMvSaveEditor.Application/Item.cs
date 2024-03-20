namespace RpgTkoolMvSaveEditor.Application;

public class Item(int id, string name, string description, int count)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Description { get; } = $"{name}\n{description}";
    public int Count { get; } = count;
}