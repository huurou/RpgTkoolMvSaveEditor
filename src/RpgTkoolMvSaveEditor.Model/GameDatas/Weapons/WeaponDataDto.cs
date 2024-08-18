namespace RpgTkoolMvSaveEditor.Model.Weapons;

/// <summary>
/// Weaponのデータ用DTO
/// www/data/Weapons.Json の配列要素
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
public record WeaponDataDto(int Id, string Name, string Description)
{
    public Weapon ToModel()
    {
        return new(new(Id), new(Name), new(Description));
    }
}