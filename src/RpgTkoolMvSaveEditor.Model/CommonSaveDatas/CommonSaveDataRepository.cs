using LZStringCSharp;
using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.GameData;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataRepository(PathProvider pathProvider, CommonSaveDataJsonObjectProvider commonSaveDataJsonObjectProvider, ILogger<CommonSaveDataRepository> logger) : ICommonSaveDataRepository
{
    public async Task<Result<CommonSaveData>> LoadAsync()
    {
        logger.LogInformation("共通セーブデータをロードしています。");
        if (pathProvider.WwwDirPath is null) { return new Err<CommonSaveData>("wwwフォルダが選択されていません。"); }
        if (!(await commonSaveDataJsonObjectProvider.GetAsync()).Unwrap(out var rootObject, out var message)) { return new Err<CommonSaveData>(message); }
        if (rootObject["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err<CommonSaveData>("セーブデータにgameSwitchesが見つかりませんでした。"); }
        if (rootObject["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err<CommonSaveData>("セーブデータにgameVariablesが見つかりませんでした。"); }
        var systemFilePath = Path.Combine(pathProvider.WwwDirPath, "data", "System.json");
        if (!File.Exists(systemFilePath)) { return new Err<CommonSaveData>($"{systemFilePath}が存在しません。"); }
        using var systemFileStream = new FileStream(systemFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var systemJsonObject = await JsonSerializer.DeserializeAsync<JsonObject>(systemFileStream);
        if (systemJsonObject is null) { return new Err<CommonSaveData>($"{systemFilePath}のロードに失敗しました。"); }
        var switchNamesJsonArray = systemJsonObject["switches"]!.AsArray();
        var variableNamesJsonArray = systemJsonObject["variables"]!.AsArray();
        var gameSwichValues = gameSwitchesJsonObject.Where(x => int.TryParse(x.Key, out _)).Select(x => (Id: int.Parse(x.Key), Value: x.Value?.GetValue<bool?>()));
        var gameSwitches = gameSwichValues.Select(x => new Switch(x.Id, switchNamesJsonArray[x.Id]!.GetValue<string>(), x.Value));
        var gameVariableValues = gameVariablesJsonObject.Where(x => int.TryParse(x.Key, out _)).Select(
            x =>
            (
                Id: int.Parse(x.Key),
                Value: x.Value?.GetValueKind() switch
                {
                    JsonValueKind.String => x.Value.GetValue<string>(),
                    JsonValueKind.Number => x.Value.GetValue<double>(),
                    JsonValueKind.True or JsonValueKind.False => x.Value.GetValue<bool>(),
                    JsonValueKind.Null => null,
                    // いずれにも一致しない場合は元のJsonNodeを返す
                    _ => (object?)x.Value,
                }
            )
        );
        var gameVariables = gameVariableValues.Select(x => new Variable(x.Id, variableNamesJsonArray[x.Id]!.GetValue<string>(), x.Value));
        logger.LogInformation("共通セーブデータがロードされました。");
        return new Ok<CommonSaveData>(new([.. gameSwitches], [.. gameVariables]));
    }

    public async Task<Result> SaveAsync(CommonSaveData commonSaveData)
    {
        logger.LogInformation("共通セーブデータをセーブしています。");
        if (pathProvider.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(pathProvider.WwwDirPath, "save", "common.rpgsave");
        if (!File.Exists(filePath)) { return new Err($"{filePath}が存在しません。"); }
        if (!(await commonSaveDataJsonObjectProvider.GetAsync()).Unwrap(out var rootObject, out var message)) { return new Err(message); }
        if (rootObject["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err("共通セーブデータにgameSwitchesが見つかりませんでした。"); }
        if (rootObject["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err("共通セーブデータにgameSwitchesが見つかりませんでした。"); }
        foreach (var gameSwitch in commonSaveData.GameSwitches)
        {
            gameSwitchesJsonObject[gameSwitch.Id.ToString()] = gameSwitch.Value;
        }
        foreach (var gameVariables in commonSaveData.GameVariables)
        {
            gameVariablesJsonObject[gameVariables.Id.ToString()] = JsonValue.Create(gameVariables.Value);
        }
        using var jsonMemoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(jsonMemoryStream, rootObject);
        jsonMemoryStream.Position = 0;
        using var jsonMemoryStreamReader = new StreamReader(jsonMemoryStream);
        var json = await jsonMemoryStreamReader.ReadToEndAsync();
        await File.WriteAllTextAsync(filePath, LZString.CompressToBase64(json));
        logger.LogInformation("共通セーブデータがセーブされました。");
        return new Ok();
    }
}
