using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateCommonVariableCommand(int Id, object? Value) : ICommand;

public class UpdateCommonVariableCommandHandler(Context context, CommonSaveDataJsonNodeStore commonSaveDataStore) : ICommandHandler<UpdateCommonVariableCommand>
{
    public async Task<Result> HandleAsync(UpdateCommonVariableCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await commonSaveDataStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err("セーブデータにgameVariablesが見つかりませんでした。"); }
        gameVariablesJsonObject[command.Id.ToString()] = JsonValue.Create(command.Value);
        return await commonSaveDataStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
