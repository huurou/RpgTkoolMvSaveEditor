using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateArmorCommand(int Id, int Count) : ICommand;

public class UpdateArmorCommandHandler(Context context, SaveDataJsonNodeStore saveDataJsonNodeStore) : ICommandHandler<UpdateArmorCommand>
{
    public async Task<Result> HandleAsync(UpdateArmorCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await saveDataJsonNodeStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["party"]?["_armors"] is not JsonObject heldArmorsJsonObject) { return new Err("セーブデータにparty::_armorsが見つかりませんでした。"); }
        heldArmorsJsonObject[command.Id.ToString()] = command.Count;
        return await saveDataJsonNodeStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
