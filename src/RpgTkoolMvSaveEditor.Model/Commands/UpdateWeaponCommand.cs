using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateWeaponCommand(int Id, int Count) : ICommand;

public class UpdateWeaponCommandHandler(Context context, SaveDataJsonNodeStore saveDataJsonNodeStore) : ICommandHandler<UpdateWeaponCommand>
{
    public async Task<Result> HandleAsync(UpdateWeaponCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await saveDataJsonNodeStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["party"]?["_weapons"] is not JsonObject heldWeaponsJsonObject) { return new Err("セーブデータにparty::_weaponsが見つかりませんでした。"); }
        heldWeaponsJsonObject[command.Id.ToString()] = command.Count;
        return await saveDataJsonNodeStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
