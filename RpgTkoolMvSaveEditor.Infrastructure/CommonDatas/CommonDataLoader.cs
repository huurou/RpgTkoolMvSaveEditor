using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;

namespace RpgTkoolMvSaveEditor.Infrastructure.CommonDatas;

public class CommonDataLoader : ICommonDataLoader
{
    private const string GAME_SWITCHES = "gameSwitches";
    private const string GAME_VARIABLES = "gameVariables";

    private readonly ISaveDataCtrl saveDataCtrl_;

    public CommonDataLoader(ISaveDataCtrl saveDataCtrl)
    {
        saveDataCtrl_ = saveDataCtrl;
    }

    public CommonData Load(string path)
    {
        var rootNode = saveDataCtrl_.Load(path);
        var switchesNode = rootNode[GAME_SWITCHES];
        var variablesNode = rootNode[GAME_VARIABLES];
        if (switchesNode is null) throw new InvalidOperationException($"{GAME_SWITCHES}の取得に失敗しました。");
        if (variablesNode is null) throw new InvalidOperationException($"{GAME_VARIABLES}の取得に失敗しました。");
        var commonData = new CommonData(new GameSwitches(switchesNode), new GameVariables(variablesNode));

        commonData.GameSwitches.PropertyChanged += (s, prop) =>
        {
            switchesNode[prop.Key.ToString()] = prop.Value;
            saveDataCtrl_.Save(path, rootNode);
        };
        commonData.GameVariables.PropertyChanged += (s, prop) =>
        {
            variablesNode[prop.Key.ToString()] = prop.Value;
            saveDataCtrl_.Save(path, rootNode);
        };

        return commonData;
    }
}