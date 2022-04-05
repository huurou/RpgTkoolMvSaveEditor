namespace RpgTkoolMvSaveEditor.Domain;

public class SystemData : GameData
{
    public string GameTitle { get; set; } = "";
    public List<string> ArmorTypes { get; set; } = new();
    public List<string> Elements { get; set; } = new();
    public List<string> EquipTypes { get; set; } = new();
    public List<string> SkillTypes { get; set; } = new();
    public List<string> Switches { get; set; } = new();
    public List<string> Variables { get; set; } = new();
}