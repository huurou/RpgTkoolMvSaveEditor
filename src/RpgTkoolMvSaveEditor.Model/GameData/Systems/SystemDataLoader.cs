using RpgTkoolMvSaveEditor.Model.GameData.Systems;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Model.Queries;

public class SystemDataLoader(Context context)
{
    private SystemDataDto? systemData_;
    private readonly JsonSerializerOptions options_ = new(JsonSerializerDefaults.Web);

    public async Task<Result<SystemDataDto>> LoadAsync()
    {
        if (context.WwwDirPath is null) { return new Err<SystemDataDto>("wwwフォルダが選択されていません。"); }
        if (systemData_ is not null) { return new Ok<SystemDataDto>(systemData_); }
        var filePath = Path.Combine(context.WwwDirPath, "data", "System.json");
        if (!File.Exists(filePath)) { return new Err<SystemDataDto>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        systemData_ = await JsonSerializer.DeserializeAsync<SystemDataDto>(fileStream, options_);
        return systemData_ is not null
            ? new Ok<SystemDataDto>(systemData_)
            : new Err<SystemDataDto>($"{filePath}のロードに失敗しました。");
    }
}
