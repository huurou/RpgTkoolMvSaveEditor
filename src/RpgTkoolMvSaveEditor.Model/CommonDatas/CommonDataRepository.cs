using LZStringCSharp;
using RpgTkoolMvSaveEditor.Model.GameDatas.Systems;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.CommonDatas;

public class CommonDataRepository(Context context, ISystemLoader systemLoader) : ICommonDataRepository
{
    public async Task<Result<CommonData>> LoadAsync()
    {
        if (context.WwwDirPath is null) { return new Err<CommonData>("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(context.WwwDirPath, "save", "common.rpgsave");
        if (!File.Exists(filePath)) { return new Err<CommonData>($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var rootNode = await JsonNode.ParseAsync(memoryStream);
        if (rootNode is null) { return new Err<CommonData>($"{filePath}のパースに失敗しました。"); }
        if (rootNode?["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err<CommonData>($"{filePath}にgameSwitches配列が見つかりませんでした。"); }
        if (rootNode?["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err<CommonData>($"{filePath}にgameVariables配列が見つかりませんでした。"); }
        if (!(await systemLoader.LoadAsync()).Unwrap(out var system, out var message)) { return new Err<CommonData>(message); }
        var gameSwitches = gameSwitchesJsonObject.ToDictionary(x => x.Key, x => x.Value?.GetValue<bool?>());
        var gameVariables = gameVariablesJsonObject.ToDictionary(
            x => x.Key,
            x => x.Value?.GetValueKind() switch
            {
                JsonValueKind.String => x.Value.GetValue<string>(),
                JsonValueKind.Number => x.Value.GetValue<int>(),
                JsonValueKind.True or JsonValueKind.False => x.Value.GetValue<bool>(),
                JsonValueKind.Null => null,
                // いずれにも一致しない場合は元のJsonNodeを返す
                _ => (object?)x,
            }
        );
        var dto = new CommonDataDataDto(gameSwitches, gameVariables);
        return new Ok<CommonData>(dto.ToModel(system));
    }

    public async Task<Result> SaveAsync(CommonData commonData)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(context.WwwDirPath, "save", "common.rpgsave");
        if (!File.Exists(filePath)) { return new Err($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var rootNode = await JsonNode.ParseAsync(memoryStream);
        if (rootNode is null) { return new Err($"{filePath}のパースに失敗しました。"); }
        if (rootNode?["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err($"{filePath}にgameSwitches配列が見つかりませんでした。"); }
        if (rootNode?["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err($"{filePath}にgameVariables配列が見つかりませんでした。"); }
        var dto = CommonDataDataDto.FromModel(commonData);
        foreach (var pair in dto.GameSwitches)
        {
            gameSwitchesJsonObject[pair.Key] = pair.Value;
        }
        foreach (var pair in dto.GameVariables)
        {
            gameVariablesJsonObject[pair.Key] = JsonValue.Create(pair.Value);
        }
        await File.WriteAllTextAsync(filePath, LZString.CompressToBase64(JsonSerializer.Serialize(rootNode)));
        return new Ok();
    }
}
