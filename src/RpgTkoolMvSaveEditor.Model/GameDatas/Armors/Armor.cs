namespace RpgTkoolMvSaveEditor.Model.Armors;

/// <summary>
/// 防具
/// </summary>
/// <param name="Id">防具ID</param>
/// <param name="Name">防具名</param>
/// <param name="Description">防具の説明</param>
public record Armor(ArmorId Id, string Name, string Description);
