using LZStringCSharp;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public class SaveDataJsonObjectProvider(PathProvider pathProvider)
{
    public async Task<Result<JsonObject>> GetAsync()
    {
        if (pathProvider.WwwDirPath is null) { return new Err<JsonObject>("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(pathProvider.WwwDirPath, "save", "file1.rpgsave");
        if (!File.Exists(filePath)) { return new Err<JsonObject>($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var jsonMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var rootNode = await JsonNode.ParseAsync(jsonMemoryStream);
        return rootNode is not null
            ? new Ok<JsonObject>(rootNode.AsObject())
            : new Err<JsonObject>($"{filePath}のパースに失敗しました。");
    }
}
