using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;

namespace RpgTkoolMvSaveEditor.Model.CommonDatas;

/// <summary>
/// ゲーム共通データのデータ用DTO
/// www/save/common.rpgsave
/// </summary>
/// <param name="Switches">スイッチのリスト インデックス www/save/file1.rpgsave::switches::_data::@a</param>
/// <param name="Variables">変数のリスト www/save/file1.rpgsave::variables::_data::@a</param>
/// <param name="GameSwitches"></param>
/// <param name="GameVariables"></param>
public record CommonDataDataDto(Dictionary<string, bool?> GameSwitches, Dictionary<string, object?> GameVariables)
{
    public static CommonDataDataDto FromModel(CommonData model)
    {
        return new(
            model.GameSwitches.ToDictionary(x => x.Id.Value.ToString(), x => x.Value.Value),
            model.GameVariables.ToDictionary(x => x.Id.Value.ToString(), x => x.Value.Value)
        );
    }

    public CommonData ToModel(GameDatas.Systems.System system)
    {
        return new(
            [.. GameSwitches.Select(x => (Id: int.Parse(x.Key), x.Value)).Select(x => new Switch(new(x.Id), system.SwitchNames[x.Id], new(x.Value)))],
            [.. GameVariables.Select(x => (Id: int.Parse(x.Key), x.Value)).Select(x => new Variable(new(x.Id), system.VariableNames[x.Id], new(x.Value)))]
        );
    }
}
