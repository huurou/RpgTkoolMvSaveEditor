using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Model.Weapons;

public class WeaponsLoader(WwwContext wwwContext) : IWeaponsLoader
{
    private readonly JsonSerializerOptions options_ = new(JsonSerializerDefaults.Web);
    private List<Weapon>? data_;

    public async Task<Result<List<Weapon>>> LoadAsync()
    {
        if (data_ is not null) { return new Ok<List<Weapon>>(data_); }
        var filePath = Path.Combine(wwwContext.WwwDirPath, "data", "Weapons.json");
        if (!File.Exists(filePath)) { return new Err<List<Weapon>>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        var dtos = await JsonSerializer.DeserializeAsync<List<WeaponDataDto?>>(fileStream, options_);
        if (dtos is not null)
        {
            data_ = [.. dtos.Where(x => x is not null).Select(x => x!.ToModel())];
            return new Ok<List<Weapon>>(data_);
        }
        else
        {
            return new Err<List<Weapon>>($"{filePath}のロードに失敗しました。");
        }
    }
}
