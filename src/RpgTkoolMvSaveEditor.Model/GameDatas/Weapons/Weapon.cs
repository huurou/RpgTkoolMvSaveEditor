namespace RpgTkoolMvSaveEditor.Model.Weapons;

/// <summary>
/// 武器
/// </summary>
/// <param name="Id">武器ID</param>
/// <param name="Name">武器名</param>
/// <param name="Description">説明</param>
public record Weapon(WeaponId Id, string Name, string Description);
