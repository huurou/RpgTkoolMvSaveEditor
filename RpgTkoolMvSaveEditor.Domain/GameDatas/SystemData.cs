namespace RpgTkoolMvSaveEditor.Domain.GameDatas;

public class SystemData : GameData
{
    public string GameTitle { get; init; } = "";
    public List<string> ArmorTypes { get; init; } = new();
    public List<string> Elements { get; init; } = new();
    public List<string> EquipTypes { get; init; } = new();
    public List<string> SkillTypes { get; init; } = new();
    public List<string> Switches { get; init; } = new();
    public List<string> Variables { get; init; } = new();
}