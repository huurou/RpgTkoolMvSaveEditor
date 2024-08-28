using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.Switches;
using RpgTkoolMvSaveEditor.Model.GameData.Variables;
using RpgTkoolMvSaveEditor.Model.Queries.Common;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Queries;

public record GetCommonSaveDataQuery() : IQuery;

public class GetCommonSaveDataQueryHandler(Context context, CommonSaveDataJsonNodeStore commonSaveDataStore, SystemDataLoader systemDataLoader, ILogger<GetCommonSaveDataQueryHandler> logger) : IQueryHandler<GetCommonSaveDataQuery, CommonSaveDataViewDto>
{
    public async Task<Result<CommonSaveDataViewDto>> HandleAsync(GetCommonSaveDataQuery query)
    {
        logger.LogInformation("Load CommonSaveData");
        if (context.WwwDirPath is null) { return new Err<CommonSaveDataViewDto>("wwwフォルダが選択されていません。"); }
        if (!(await commonSaveDataStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err<CommonSaveDataViewDto>(message); }
        if (rootNode["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err<CommonSaveDataViewDto>("セーブデータにgameSwitchesが見つかりませんでした。"); }
        if (rootNode["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err<CommonSaveDataViewDto>("セーブデータにgameVariablesが見つかりませんでした。"); }
        if (!(await systemDataLoader.LoadAsync()).Unwrap(out var systemData, out message)) { return new Err<CommonSaveDataViewDto>(message); }
        var gameSwichValues = gameSwitchesJsonObject.Where(x => int.TryParse(x.Key, out _)).Select(x => (Id: int.Parse(x.Key), Value: x.Value?.GetValue<bool?>()));
        var gameSwitches = gameSwichValues.Select(x => new SwitchViewDto(x.Id, systemData.Switches[x.Id], x.Value));
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
        var gameVariables = gameVariableValues.Select(x => new VariableViewDto(x.Id, systemData.Variables[x.Id], x.Value));
        return new Ok<CommonSaveDataViewDto>(new([.. gameSwitches], [.. gameVariables]));
    }
}