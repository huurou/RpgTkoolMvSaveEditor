using LZStringCSharp;
using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.GameData;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataRepository(Context context, ILogger<CommonSaveDataRepository> logger) : ICommonSaveDataRepository
{
    private JsonArray? switchNamesJsonArray_; // ["switch1",]
    private JsonArray? variableNamesJsonArray_; // ["variable1",]

    public async Task<Result<CommonSaveData>> LoadAsync()
    {
        logger.LogInformation("Load CommonSaveData");
        if (context.WwwDirPath is null) { return new Err<CommonSaveData>("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(context.WwwDirPath, "save", "common.rpgsave");
        if (!File.Exists(filePath)) { return new Err<CommonSaveData>($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var jsonMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var rootNode = await JsonNode.ParseAsync(jsonMemoryStream);
        if (rootNode is null) { return new Err<CommonSaveData>($"{filePath}のパースに失敗しました。"); }
        if (rootNode["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err<CommonSaveData>("セーブデータにgameSwitchesが見つかりませんでした。"); }
        if (rootNode["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err<CommonSaveData>("セーブデータにgameVariablesが見つかりませんでした。"); }
        context.commonSaveDataRootNode_ = rootNode;
        if (switchNamesJsonArray_ is null || variableNamesJsonArray_ is null)
        {
            var systemFilePath = Path.Combine(context.WwwDirPath, "data", "System.json");
            if (!File.Exists(systemFilePath)) { return new Err<CommonSaveData>($"{systemFilePath}が存在しません。"); }
            using var fileStream = new FileStream(systemFilePath, FileMode.Open);
            var systemJsonObject = await JsonSerializer.DeserializeAsync<JsonObject>(fileStream);
            if (systemJsonObject is null) { return new Err<CommonSaveData>($"{systemFilePath}のロードに失敗しました。"); }
            switchNamesJsonArray_ = systemJsonObject["switches"]!.AsArray();
            variableNamesJsonArray_ = systemJsonObject["variables"]!.AsArray();
        }
        var gameSwichValues = gameSwitchesJsonObject.Where(x => int.TryParse(x.Key, out _)).Select(x => (Id: int.Parse(x.Key), Value: x.Value?.GetValue<bool?>()));
        var gameSwitches = gameSwichValues.Select(x => new Switch(x.Id, switchNamesJsonArray_[x.Id]!.GetValue<string>(), x.Value));
        var gameVariableValues = gameVariablesJsonObject.Where(x => int.TryParse(x.Key, out _)).Select(
            x =>
            (
                Id: int.Parse(x.Key),
                Value: x.Value?.GetValueKind() switch
                {
                    JsonValueKind.String => x.Value.GetValue<string>(),
                    JsonValueKind.Number => x.Value.GetValue<int>(),
                    JsonValueKind.True or JsonValueKind.False => x.Value.GetValue<bool>(),
                    JsonValueKind.Null => null,
                    // いずれにも一致しない場合は元のJsonNodeを返す
                    _ => (object?)x.Value,
                }
            )
        );
        var gameVariables = gameVariableValues.Select(x => new Variable(x.Id, variableNamesJsonArray_[x.Id]!.GetValue<string>(), x.Value));
        return new Ok<CommonSaveData>(new([.. gameSwitches], [.. gameVariables]));
    }

    public async Task<Result> SaveAsync(CommonSaveData commonSaveData)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(context.WwwDirPath, "save", "common.rpgsave");
        if (!File.Exists(filePath)) { return new Err($"{filePath}が存在しません。"); }
        if (context.commonSaveDataRootNode_ is null) { return new Err("共通セーブデータが未ロードです。"); }
        if (context.commonSaveDataRootNode_["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err("共通セーブデータにgameSwitchesが見つかりませんでした。"); }
        if (context.commonSaveDataRootNode_["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err("共通セーブデータにgameSwitchesが見つかりませんでした。"); }
        foreach (var gameSwitch in commonSaveData.GameSwitches)
        {
            gameSwitchesJsonObject[gameSwitch.Id.ToString()] = gameSwitch.Value;
        }
        foreach (var gameVariables in commonSaveData.GameVariables)
        {
            gameVariablesJsonObject[gameVariables.Id.ToString()] = JsonValue.Create(gameVariables.Value);
        }
        using var jsonMemoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(jsonMemoryStream, context.commonSaveDataRootNode_);
        jsonMemoryStream.Position = 0;
        using var jsonMemoryStreamReader = new StreamReader(jsonMemoryStream);
        var json = await jsonMemoryStreamReader.ReadToEndAsync();
        await File.WriteAllTextAsync(filePath, LZString.CompressToBase64(json));
        return new Ok();
    }
}
