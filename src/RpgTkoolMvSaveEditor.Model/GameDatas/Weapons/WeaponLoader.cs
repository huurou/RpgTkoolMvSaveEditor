using System.Collections.Immutable;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Model.Weapons;

public record WeaponLoader
{
    public async Task<ImmutableList<Weapon>> LoadAsync(string weaponJsonPath)
    {
        using var fileStream = new FileStream(weaponJsonPath, FileMode.Open);
        return [
            .. (await JsonSerializer.DeserializeAsync<List<WeaponDataDto>>(fileStream))
                ?.Where(x=> x is not null).Select(x=>x.ToModel())
        ];
    }
}