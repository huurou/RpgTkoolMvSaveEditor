using LZStringCSharp;
using RpgTkoolMvSaveEditor.Domain;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure;

public class SaveDataSerializer : ISaveDataSerializer
{
    public void Serialize(string path, JsonNode jsonNode)
    {
        var jsonStr = jsonNode.ToJsonString();
        File.WriteAllText(path, LZString.CompressToBase64(jsonStr));
    }

    public JsonNode Deserialize(string path)
    {
        var jsonStr = LZString.DecompressFromBase64(File.ReadAllText(path));
        return JsonNode.Parse(jsonStr)!;
    }
}