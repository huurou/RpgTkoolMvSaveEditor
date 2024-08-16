namespace RpgTkoolMvSaveEditor.Model.Weapons;

/// <summary>
/// 武器
/// www/data/Weapons.Json の配列要素
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
public record Weapon(WeaponId Id, WeaponName Name, WeaponDescription Description);