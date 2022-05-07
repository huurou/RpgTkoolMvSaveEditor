using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class SaveDataLoader : ISaveDataLoader
{
    private readonly ISaveDataCtrl saveDataCtrl_;

    public SaveDataLoader(ISaveDataCtrl saveDataCtrl)
    {
        saveDataCtrl_ = saveDataCtrl;
    }

    public async Task<SaveData?> LoadAsync(string path)
    {
        if (!File.Exists(path)) return null;

        var rootNode = await saveDataCtrl_.LoadAsync(path);

        var switchesNode = rootNode["switches"];
        if (switchesNode is not null) switchesNode = switchesNode["_data"];
        if (switchesNode is not null) switchesNode = switchesNode["@a"];

        var variablesNode = rootNode["variables"];
        if (variablesNode is not null) variablesNode = variablesNode["_data"];
        if (variablesNode is not null) variablesNode = variablesNode["@a"];

        var partyNode = rootNode["party"];

        JsonNode? itemsNode = null;
        if (partyNode is not null) itemsNode = partyNode["_items"];

        JsonNode? weaponsNode = null;
        if (partyNode is not null) weaponsNode = partyNode["_weapons"];

        JsonNode? armorsNode = null;
        if (partyNode is not null) armorsNode = partyNode["_armors"];

        var saveData = new SaveData(new Parameters(partyNode!["_gold"]),
                                    new Switches(switchesNode),
                                    new Variables(variablesNode),
                                    new Items(itemsNode),
                                    new Weapons(weaponsNode),
                                    new Armors(armorsNode));

        saveData.Parameters.GoldChanged += async (s, gold) =>
        {
            if (partyNode is null) return;
            partyNode["_gold"] = gold;
            await saveDataCtrl_.SaveAsync(path, rootNode);
        };

        saveData.Switches.ValueChanged += async (s, e) =>
        {
            if (switchesNode is null) return;
            var switchesArray = switchesNode.AsArray();
            var count = switchesArray.Count;
            if (e.index >= count)
            {
                for (var i = 0; i < e.index - count + 1; i++)
                {
                    switchesArray.Add(null);
                }
            }
            switchesArray[e.index] = e.value;
            await saveDataCtrl_.SaveAsync(path, rootNode);
        };

        saveData.Variables.ValueChanged += async (s, e) =>
        {
            if (variablesNode is null) return;
            var variablesArray = variablesNode.AsArray();
            var count = variablesArray.Count;
            if (e.index >= count)
            {
                for (var i = 0; i < e.index - count + 1; i++)
                {
                    variablesArray.Add(null);
                }
            }
            variablesNode[e.index] = e.value switch
            {
                string str => int.TryParse(str, out var i) ? i
                    : double.TryParse(str, out var d) ? d
                    : str,
                int num => num,
                double dou => dou,
                bool b => b,
                _ => null,
            };
            await saveDataCtrl_.SaveAsync(path, rootNode);
        };

        saveData.Items.ValueChanged += async (s, e) =>
        {
            if (itemsNode is null) return;
            itemsNode[e.id] = e.value;
            await saveDataCtrl_.SaveAsync(path, rootNode);
        };

        saveData.Weapons.ValueChanged += async (s, e) =>
        {
            if (weaponsNode is null) return;
            weaponsNode[e.id] = e.value;
            await saveDataCtrl_.SaveAsync(path, rootNode);
        };

        saveData.Armors.ValueChanged += async (s, e) =>
        {
            if (armorsNode is null) return;
            armorsNode[e.id] = e.value;
            await saveDataCtrl_.SaveAsync(path, rootNode);
        };

        return saveData;
    }
}