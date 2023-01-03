namespace RpgTkoolMvSaveEditor.Application;

public class Switch
{
    public int Id { get; }
    public string Name { get; }
    public bool? Value { get; }

    public Switch(int id, string name, bool? value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}