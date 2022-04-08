namespace RpgTkoolMvSaveEditor.Application;

public class Armor
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Count { get; }

    public Armor(int id, string name, string description, int count)
    {
        Id = id;
        Name = name;
        Description = $"{name}\n{description}";
        Count = count;
    }
}