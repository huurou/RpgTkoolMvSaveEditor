using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.SaveDatas;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class SaveDataLoader : ISaveDataLoader
{
    private readonly ISaveDataCtrl saveDataCtrl_;

    public SaveDataLoader(ISaveDataCtrl saveDataCtrl)
    {
        saveDataCtrl_ = saveDataCtrl;
    }

    public SaveData? Load(string path)
    {
        if (!File.Exists(path)) return null;

        var rootNode = saveDataCtrl_.Load(path);

        var switchesNode = rootNode["switches"];
        if (switchesNode is null) throw new InvalidOperationException("switchesの取得に失敗しました。");
        switchesNode = switchesNode["_data"];
        if (switchesNode is null) throw new InvalidOperationException("switches::_dataの取得に失敗しました。");
        switchesNode = switchesNode["@a"];
        if (switchesNode is null) throw new InvalidOperationException("switches::_data::@aの取得に失敗しました。");

        var variablesNode = rootNode["variables"];
        if (variablesNode is null) throw new InvalidOperationException("variablesの取得に失敗しました。");
        variablesNode = variablesNode["_data"];
        if (variablesNode is null) throw new InvalidOperationException("variables::_dataの取得に失敗しました。");
        variablesNode = variablesNode["@a"];
        if (variablesNode is null) throw new InvalidOperationException("variables::_data::@aの取得に失敗しました。");

        var itemsNode = rootNode["party"];
        if (itemsNode is null) throw new InvalidOperationException("partyの取得に失敗しました。");
        itemsNode = itemsNode["_items"];
        if (itemsNode is null) throw new InvalidOperationException("party::_itemsの取得に失敗しました。");

        var armorsNode = rootNode["party"];
        if (armorsNode is null) throw new InvalidOperationException("partyの取得に失敗しました。");
        armorsNode = armorsNode["_armors"];
        if (armorsNode is null) throw new InvalidOperationException("party::_armorsの取得に失敗しました。");

        var saveData = new SaveData(new Switches(switchesNode),
                                    new Variables(variablesNode),
                                    new Items(itemsNode),
                                    new Armors(armorsNode));

        saveData.Switches.PropertyChanged += (s, prop) =>
        {
            var switchesArray = switchesNode.AsArray();
            var count = switchesArray.Count;
            if (prop.index >= count)
            {
                for (var i = 0; i < prop.index - count + 1; i++)
                {
                    switchesArray.Add(null);
                }
            }
            switchesArray[prop.index] = prop.value;
            saveDataCtrl_.Save(path, rootNode);
        };

        saveData.Variables.PropertyChanged += (s, prop) =>
        {
            var variablesArray = variablesNode.AsArray();
            var count = variablesArray.Count;
            if (prop.index >= count)
            {
                for (var i = 0; i < prop.index - count + 1; i++)
                {
                    variablesArray.Add(null);
                }
            }
            variablesNode[prop.index] = prop.value switch
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

        saveData.Items.PropertyChanged += (s, prop) =>
        {
            itemsNode[prop.id] = prop.value;
            saveDataCtrl_.Save(path, rootNode);
        };

        saveData.Armors.PropertyChanged += (s, prop) =>
        {
            armorsNode[prop.id] = prop.value;
            saveDataCtrl_.Save(path, rootNode);
        };

        return saveData;
    }
}