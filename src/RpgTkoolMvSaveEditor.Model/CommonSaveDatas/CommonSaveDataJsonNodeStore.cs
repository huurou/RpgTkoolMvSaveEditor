using LZStringCSharp;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataJsonNodeStore
{
    private readonly Context context_;
    private readonly System.Timers.Timer delayTimer_ = new(100);
    private string? wwwDirPath_;
    private JsonNode? rootNode_;

    public CommonSaveDataJsonNodeStore(Context context)
    {
        context_ = context;

        delayTimer_.Elapsed += async (s, e) => await SaveInnerAsync();
    }

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

    public void Save(string wwwDirPath, JsonNode rootNode)
    {
        wwwDirPath_ = wwwDirPath;
        rootNode_ = rootNode;
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

    private async Task SaveInnerAsync()
    {
        if (string.IsNullOrEmpty(wwwDirPath_) || rootNode_ is null) { return; }
        var filePath = Path.Combine(wwwDirPath_, "save", "common.rpgsave");
        using var jsonMemoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(jsonMemoryStream, rootNode_);
        jsonMemoryStream.Position = 0;
        using var jsonMemoryStreamReader = new StreamReader(jsonMemoryStream);
        var json = await jsonMemoryStreamReader.ReadToEndAsync();
        await File.WriteAllTextAsync(filePath, LZString.CompressToBase64(json));
        context_.CommonSaveDataLoadSuppressed = true;
    }
}