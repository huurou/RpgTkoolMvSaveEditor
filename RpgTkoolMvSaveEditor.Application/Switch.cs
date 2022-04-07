namespace RpgTkoolMvSaveEditor.Application;

public class Switch
{
    public string Id { get; }
    public string Name { get; }
    public bool? Value { get; }

    public Switch(string id, string name, bool? value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}