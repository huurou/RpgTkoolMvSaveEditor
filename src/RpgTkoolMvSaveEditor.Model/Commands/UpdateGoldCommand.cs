using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Commands;

public record UpdateGoldCommand(int Value) : ICommand;

public class UpdateGoldCommandHandler(Context context, SaveDataJsonNodeStore saveDataJsonNodeStore) : ICommandHandler<UpdateGoldCommand>
{
    public async Task<Result> HandleAsync(UpdateGoldCommand command)
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        if (!(await saveDataJsonNodeStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err(message); }
        if (rootNode["party"]?["_gold"] is not JsonNode goldJsonNode) { return new Err("セーブデータにparty::_goldが見つかりませんでした。"); }
        goldJsonNode.AsValue().ReplaceWith(command.Value);
        return await saveDataJsonNodeStore.SaveAsync(context.WwwDirPath, rootNode);
    }
}
