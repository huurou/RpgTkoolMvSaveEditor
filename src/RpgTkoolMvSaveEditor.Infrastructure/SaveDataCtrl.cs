using LZStringCSharp;
using RpgTkoolMvSaveEditor.Domain;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure;

public class SaveDataCtrl : ISaveDataCtrl
{
    // 保存を遅延させ、連続で保存要求が来た場合直前の保存をキャンセルする
    private readonly System.Timers.Timer delayTimer_ = new(100) { AutoReset = false };
    private string path_ = "";
    private JsonNode? jsonNode_ = null;

    public SaveDataCtrl()
    {
        delayTimer_.Elapsed += (s, e) => File.WriteAllTextAsync(path_, LZString.CompressToBase64(jsonNode_?.ToJsonString()));
    }

    public void Save(string path, JsonNode jsonNode)
    {
        path_ = path;
        jsonNode_ = jsonNode;
        if (delayTimer_.Enabled)
        {
            delayTimer_.Stop();
            delayTimer_.Start();
        }
        else
        {
            delayTimer_.Start();
        }
    }

    public async Task<JsonNode> LoadAsync(string path)
    {
        var jsonStr = LZString.DecompressFromBase64(File.ReadAllText(path));
        return await Task.Run(() => JsonNode.Parse(jsonStr)!);
    }
}