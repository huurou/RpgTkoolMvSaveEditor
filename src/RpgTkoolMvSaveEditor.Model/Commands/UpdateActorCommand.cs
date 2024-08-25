using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateActorCommand(int Id, string Name, int HP, int MP, int TP, int Level, int Exp) : ICommand;

public class UpdateActorCommandHandler(Context context, SaveDataJsonNodeStore saveDataJsonNodeStore) : ICommandHandler<UpdateActorCommand>
{
    public async Task<Result> HandleAsync(UpdateActorCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await saveDataJsonNodeStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["actors"]?["_data"]?["@a"] is not JsonArray actorsJsonArray) { return new Err("セーブデータにactors::_data::@aが見つかりませんでした。"); }
        var actorJsonObject = actorsJsonArray[command.Id]?.AsObject();
        if (actorJsonObject is null) { return new Err($"指定Id:{command.Id}のアクターが存在しません。"); }
        actorJsonObject["_name"] = command.Name;
        actorJsonObject["_hp"] = command.HP;
        actorJsonObject["_mp"] = command.MP;
        actorJsonObject["_tp"] = command.TP;
        actorJsonObject["_level"] = command.Level;
        actorJsonObject["_exp"]!["1"] = command.Exp;
        return await saveDataJsonNodeStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
