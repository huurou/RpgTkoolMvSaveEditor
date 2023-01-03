namespace RpgTkoolMvSaveEditor.Application;

public class Weapon
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Count { get; }

    public Weapon(int id, string name, string description, int count)
    {
        Id = id;
        Name = name;
        Description = description;
        Count = count;
    }
}