using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Model.Armors;

public class ArmorsLoader(Context context) : IArmorsLoader
{
    private readonly JsonSerializerOptions options_ = new(JsonSerializerDefaults.Web);
    private List<Armor>? data_;

    public async Task<Result<List<Armor>>> LoadAsync()
    {
        if (data_ is not null) { return new Ok<List<Armor>>(data_); }
        if (context.WwwDirPath is null) { return new Err<List<Armor>>("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(context.WwwDirPath, "data", "Armors.json");
        if (!File.Exists(filePath)) { return new Err<List<Armor>>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        var dtos = await JsonSerializer.DeserializeAsync<List<ArmorDataDto?>>(fileStream, options_);
        if (dtos is not null)
        {
            data_ = [.. dtos.Where(x => x is not null).Select(x => x!.ToModel())];
            return new Ok<List<Armor>>(data_);
        }
        else
        {
            return new Err<List<Armor>>($"{filePath}のロードに失敗しました。");
        }
    }
}
