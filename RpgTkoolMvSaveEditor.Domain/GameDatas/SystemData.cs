namespace RpgTkoolMvSaveEditor.Domain.GameDatas;

public class SystemData : GameData
{
    public string GameTitle { get; } = "";
    public List<string> ArmorTypes { get; } = new();
    public List<string> Elements { get; } = new();
    public List<string> EquipTypes { get; } = new();
    public List<string> SkillTypes { get; } = new();
    public List<string> Switches { get; } = new();
    public List<string> Variables { get; } = new();
}