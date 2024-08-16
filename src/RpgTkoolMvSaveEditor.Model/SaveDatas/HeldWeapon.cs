using RpgTkoolMvSaveEditor.Model.Weapons;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

/// <summary>
/// 所持武器
/// www/save/file1.rpgsave::party::_weapons の配列要素
/// </summary>
/// <param name="Weapon">武器</param>
/// <param name="Count">所持数</param>
public record HeldWeapon(Weapon Weapon, int Count);
