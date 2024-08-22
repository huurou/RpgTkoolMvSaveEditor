using RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

namespace RpgTkoolMvSaveEditor.Model.GameDatas.Weapons;

public record WeaponViewDto(string Name, string Description, int HeldCount)
{
    public static WeaponViewDto FromModel(HeldWeapon model)
    {
        return new(model.Weapon.Name, model.Weapon.Description, model.Count);
    }

    public HeldWeapon ToModel(int id)
    {
        return new(new(new(id), Name, Description), HeldCount);
    }
}
