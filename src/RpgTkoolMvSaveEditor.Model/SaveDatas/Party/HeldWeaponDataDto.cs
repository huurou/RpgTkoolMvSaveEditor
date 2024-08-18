using RpgTkoolMvSaveEditor.Model.Weapons;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

/// <summary>
/// 所持武器のデータ用DTO
/// www/save/file1.rpgsave::party::_weapons の配列要素
/// </summary>
/// <param name="Id">武器ID</param>
/// <param name="Count">所持数</param>
public record HeldWeaponDataDto(int Id, int Count)
{
    public static HeldWeaponDataDto FromModel(HeldWeapon model)
    {
        return new(model.Weapon.Id.Value, model.Count);
    }

    public HeldWeapon ToModel(IEnumerable<Weapon> weapons)
    {
        return new(weapons.First(x => x.Id.Value == Id), Count);
    }
}
