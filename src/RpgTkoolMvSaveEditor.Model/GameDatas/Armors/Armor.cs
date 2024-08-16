namespace RpgTkoolMvSaveEditor.Model.Armors;

/// <summary>
/// 防具
/// www/data/Armors.Json の配列要素
/// </summary>
/// <param name="Id">防具ID</param>
/// <param name="Name">防具名</param>
/// <param name="Description">防具の説明</param>
public record Armor(ArmorId Id, ArmorName Name, ArmorDescription Description);
