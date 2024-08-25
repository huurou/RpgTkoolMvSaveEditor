using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateSwitchCommand(int Id, bool? Value) : ICommand;

public class UpdateSwitchCommandHandler(Context context, SaveDataJsonNodeStore saveDataJsonNodeStore) : ICommandHandler<UpdateSwitchCommand>
{
    public async Task<Result> HandleAsync(UpdateSwitchCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await saveDataJsonNodeStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["switches"]?["_data"]?["@a"] is not JsonArray switchValuesJsonArray) { return new Err("セーブデータにswitches::_data::@aが見つかりませんでした。"); }
        // セーブデータのスイッチ配列は要素数が全スイッチ数より少ないことがあるので足りない分だけ増やす
        while (command.Id >= switchValuesJsonArray.Count)
        {
            switchValuesJsonArray.Add(null);
        }
        switchValuesJsonArray[command.Id] = command.Value;
        return await saveDataJsonNodeStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
