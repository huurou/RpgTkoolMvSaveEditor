using LZStringCSharp;
using RpgTkoolMvSaveEditor.Domain;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure;

public class SaveDataCtrl : ISaveDataCtrl
{
    public async Task SaveAsync(string path, JsonNode jsonNode)
    {
        var jsonStr = jsonNode.ToJsonString();
        await File.WriteAllTextAsync(path, LZString.CompressToBase64(jsonStr));
    }

    public async Task<JsonNode> LoadAsync(string path)
    {
        var jsonStr = LZString.DecompressFromBase64(File.ReadAllText(path));
        return await Task.Run(() => JsonNode.Parse(jsonStr)!);
    }
}