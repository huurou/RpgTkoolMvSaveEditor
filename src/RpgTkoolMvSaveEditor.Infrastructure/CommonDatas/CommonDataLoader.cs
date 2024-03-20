using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;

namespace RpgTkoolMvSaveEditor.Infrastructure.CommonDatas;

public class CommonDataLoader(ISaveDataCtrl saveDataCtrl) : ICommonDataLoader
{
    private readonly ISaveDataCtrl saveDataCtrl_ = saveDataCtrl;

    public async Task<CommonData?> LoadAsync(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        var rootNode = await saveDataCtrl_.LoadAsync(path);

        var switchesNode = rootNode["gameSwitches"] ?? throw new InvalidOperationException("gameSwitchesの取得に失敗しました。");
        var variablesNode = rootNode["gameVariables"] ?? throw new InvalidOperationException("gameVariablesの取得に失敗しました。");
        var commonData = new CommonData(new GameSwitches(switchesNode), new GameVariables(variablesNode));

        commonData.GameSwitches.PropertyChanged += (s, prop) =>
       {
           switchesNode[prop.Key.ToString()] = prop.Value;
           saveDataCtrl_.Save(path, rootNode);
       };

        commonData.GameVariables.PropertyChanged += (s, prop) =>
       {
           variablesNode[prop.Key] = prop.Value switch
           {
               string str => int.TryParse(str, out var i) ? i
                   : double.TryParse(str, out var d) ? d
                   : str,
               int num => num,
               double dou => dou,
               bool b => b,
               _ => null,
           };
           saveDataCtrl_.Save(path, rootNode);
       };

        return commonData;
    }
}