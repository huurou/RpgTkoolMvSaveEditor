using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateCommonSwitchCommand(int Id, bool? Value) : ICommand;

public class UpdateCommonSwitchCommandHandler(Context context, CommonSaveDataJsonNodeStore commonSaveDataStore) : ICommandHandler<UpdateCommonSwitchCommand>
{
    public async Task<Result> HandleAsync(UpdateCommonSwitchCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await commonSaveDataStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["gameSwitches"] is not JsonObject gameSwitchesJsonObject) { return new Err("セーブデータにgameSwitchesが見つかりませんでした。"); }
        gameSwitchesJsonObject[command.Id.ToString()] = command.Value;
        return await commonSaveDataStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
