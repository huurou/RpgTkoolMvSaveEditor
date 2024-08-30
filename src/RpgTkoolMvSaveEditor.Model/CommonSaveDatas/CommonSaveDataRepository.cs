﻿using LZStringCSharp;
using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.GameData;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataRepository(PathProvider pathProvider, CommonSaveDataJsonObjectProvider commonSaveDataJsonObjectProvider, ILogger<CommonSaveDataRepository> logger) : ICommonSaveDataRepository
{
    private JsonArray? switchNamesJsonArray_; // ["switch1",]
    private JsonArray? variableNamesJsonArray_; // ["variable1",]

    public async Task<Result<CommonSaveData>> LoadAsync()
    {
        logger.LogInformation("Load CommonSaveData");
        if (pathProvider.WwwDirPath is null) { return new Err<CommonSaveData>("wwwフォルダが選択されていません。"); }
        if (!(await commonSaveDataJsonObjectProvider.GetAsync()).Unwrap(out var rootObject, out var message)) { return new Err<CommonSaveData>(message); }
        if (rootObject["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err<CommonSaveData>("セーブデータにgameSwitchesが見つかりませんでした。"); }
        if (rootObject["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err<CommonSaveData>("セーブデータにgameVariablesが見つかりませんでした。"); }
        if (switchNamesJsonArray_ is null || variableNamesJsonArray_ is null)
        {
            var systemFilePath = Path.Combine(pathProvider.WwwDirPath, "data", "System.json");
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
        return new Ok();
    }
}