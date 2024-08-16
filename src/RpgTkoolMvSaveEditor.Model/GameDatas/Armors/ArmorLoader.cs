using System.Collections.Immutable;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Model.Armors;

public record ArmorLoader
{
    public async Task<ImmutableList<Armor>> Load(string armorJsonPath)
    {
        var fileStream = new FileStream(armorJsonPath, FileMode.Open);
        return [
            .. (await JsonSerializer.DeserializeAsync<List<ArmorDataDto>>(fileStream))
                ?.Where(x => x is not null).Select(x => x.ToModel())
        ];
    }
}