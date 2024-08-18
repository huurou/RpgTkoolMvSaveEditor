using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.Weapons;

public interface IWeaponsLoader
{
    Task<Result<List<Weapon>>> LoadAsync();
}
