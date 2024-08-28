using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateGameVariableCommand(int Id, object? Value) : ICommand;

public class UpdateGameVariableCommandHandler(Context context, CommonSaveDataJsonNodeStore commonSaveDataStore) : ICommandHandler<UpdateGameVariableCommand>
{
    public async Task<Result> HandleAsync(UpdateGameVariableCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await commonSaveDataStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["gameVariables"] is not JsonObject gameVariablesJsonObject) { return new Err("セーブデータにgameVariablesが見つかりませんでした。"); }
        gameVariablesJsonObject[command.Id.ToString()] = JsonValue.Create(command.Value);
        commonSaveDataStore.Save(context.WwwDirPath, rootNode);
        return new Ok();
    }
}
