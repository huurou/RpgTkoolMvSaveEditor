using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Model.Items;

public class ItemsLoader(Context context) : IItemsLoader
{
    private readonly JsonSerializerOptions options_ = new(JsonSerializerDefaults.Web);
    private List<Item>? data_;

    public async Task<Result<List<Item>>> LoadAsync()
    {
        if (data_ is not null) { return new Ok<List<Item>>(data_); }
        if (context.WwwDirPath is null) { return new Err<List<Item>>("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(context.WwwDirPath, "data", "Items.json");
        if (!File.Exists(filePath)) { return new Err<List<Item>>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        var dtos = await JsonSerializer.DeserializeAsync<List<ItemDataDto?>>(fileStream, options_);
        if (dtos is not null)
        {
            data_ = [.. dtos.Where(x => x is not null).Select(x => x!.ToModel())];
            return new Ok<List<Item>>(data_);
        }
        else
        {
            return new Err<List<Item>>($"{filePath}のロードに失敗しました。");
        }
    }
}
