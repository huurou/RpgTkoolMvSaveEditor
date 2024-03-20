namespace RpgTkoolMvSaveEditor.Domain.GameDatas;

public class SystemData
{
    public string GameTitle { get; init; } = "";
    public List<string> ArmorTypes { get; init; } = [];
    public List<string> Elements { get; init; } = [];
    public List<string> EquipTypes { get; init; } = [];
    public List<string> SkillTypes { get; init; } = [];
    public List<string> Switches { get; init; } = [];
    public List<string> Variables { get; init; } = [];
}