using LZStringCSharp;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataJsonNodeStore
{
    public async Task<Result<JsonNode>> LoadAsync(string wwwDirPath)
    {
        var filePath = Path.Combine(wwwDirPath, "save", "common.rpgsave");
        if (!File.Exists(filePath)) { return new Err<JsonNode>($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var rootNode = await JsonNode.ParseAsync(memoryStream);
        if (rootNode is null) { return new Err<JsonNode>($"{filePath}のパースに失敗しました。"); }
        return new Ok<JsonNode>(rootNode);
    }

    public async Task<Result> SaveAsync(string wwwDirPath, JsonNode rootNode)
    {
        var filePath = Path.Combine(wwwDirPath, "save", "common.rpgsave");
        using var jsonMemoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(jsonMemoryStream, rootNode);
        jsonMemoryStream.Position = 0;
        using var jsonMemoryStreamReader = new StreamReader(jsonMemoryStream);
        var json = await jsonMemoryStreamReader.ReadToEndAsync();
        await File.WriteAllTextAsync(filePath, LZString.CompressToBase64(json));
        return new Ok();
    }
}