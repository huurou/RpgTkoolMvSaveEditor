using RpgTkoolMvSaveEditor.Model.Weapons;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

/// <summary>
/// 所持武器
/// </summary>
/// <param name="Weapon">武器</param>
/// <param name="Count">所持数</param>
public record HeldWeapon(Weapon Weapon, int Count);
