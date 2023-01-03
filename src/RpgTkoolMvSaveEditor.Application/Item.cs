namespace RpgTkoolMvSaveEditor.Application;

public class Item
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Count { get; }

    public Item(int id, string name, string description, int count)
    {
        Id = id;
        Name = name;
        Description = $"{name}\n{description}";
        Count = count;
    }
}