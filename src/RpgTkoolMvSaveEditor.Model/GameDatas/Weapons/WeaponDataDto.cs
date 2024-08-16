namespace RpgTkoolMvSaveEditor.Model.Weapons;

public record WeaponDataDto(int Id, string Name, string Description)
{
    public Weapon ToModel()
    {
        return new(new(Id), new(Name), new(Description));
    }
}
