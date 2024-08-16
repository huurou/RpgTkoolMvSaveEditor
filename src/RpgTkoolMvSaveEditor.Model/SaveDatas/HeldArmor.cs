using RpgTkoolMvSaveEditor.Model.Armors;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

/// <summary>
/// 所持防具
/// www/save/file1.rpgsave::party::_armors の配列要素
/// </summary>
/// <param name="Armor">防具</param>
/// <param name="Count">所持数</param>
public record HeldArmor(Armor Armor, int Count);
