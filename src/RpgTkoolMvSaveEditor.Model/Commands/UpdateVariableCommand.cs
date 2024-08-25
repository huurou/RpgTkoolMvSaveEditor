using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateVariableCommand(int Id, object? Value) : ICommand;

public class UpdateVariableCommandHandler(Context context, SaveDataJsonNodeStore saveDataJsonNodeStore) : ICommandHandler<UpdateVariableCommand>
{
    public async Task<Result> HandleAsync(UpdateVariableCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await saveDataJsonNodeStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["variables"]?["_data"]?["@a"] is not JsonArray variablesValuesJsonArray) { return new Err("セーブデータにvariables::_data::@aが見つかりませんでした。"); }
        // セーブデータの変数配列は要素数が全変数数より少ないことがあるので足りない分だけ増やす
        while (command.Id >= variablesValuesJsonArray.Count)
        {
            variablesValuesJsonArray.Add(null);
        }
        variablesValuesJsonArray[command.Id] = JsonValue.Create(command.Value);
        return await saveDataJsonNodeStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
