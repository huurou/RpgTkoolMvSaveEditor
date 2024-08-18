using LZStringCSharp;
using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.GameDatas.Systems;
using RpgTkoolMvSaveEditor.Model.Items;
using RpgTkoolMvSaveEditor.Model.SaveDatas.Actors;
using RpgTkoolMvSaveEditor.Model.SaveDatas.Party;
using RpgTkoolMvSaveEditor.Model.Weapons;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public class SaveDataRepository(WwwContext wwwContext, ISystemLoader systemLoader, IItemsLoader itemsLoader, IWeaponsLoader weaponsLoader, IArmorsLoader armorsLoader) : ISaveDataRepository
{
    public string CommonSaveDataPath => Path.Combine(wwwContext.WwwDirPath, "save", "common.rpgsave");

    public async Task<Result<SaveData>> LoadAsync()
    {
        var filePath = Path.Combine(wwwContext.WwwDirPath, "save", "file1.rpgsave");
        if (!File.Exists(filePath)) { return new Err<SaveData>($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var rootNode = await JsonNode.ParseAsync(memoryStream);
        if (rootNode?["switches"]?["_data"]?["@a"] is not JsonArray switchesJsonArray) { return new Err<SaveData>($"{filePath}にswitches配列が見つかりませんでした。"); }
        if (rootNode?["variables"]?["_data"]?["@a"] is not JsonArray variablesJsonArray) { return new Err<SaveData>($"{filePath}にvariables配列が見つかりませんでした。"); }
        if (rootNode?["actors"]?["_data"]?["@a"] is not JsonArray actorsJsonArray) { return new Err<SaveData>($"{filePath}にactors配列が見つかりませんでした。"); }
        if (rootNode?["party"]?["_gold"] is not JsonNode goldJsonNode) { return new Err<SaveData>($"{filePath}にgold要素が見つかりませんでした。"); }
        if (rootNode?["party"]?["_items"] is not JsonObject heldItemsJsonObject) { return new Err<SaveData>($"{filePath}にitems配列が見つかりませんでした。"); }
        if (rootNode?["party"]?["_weapons"] is not JsonObject heldWeaponsJsonObject) { return new Err<SaveData>($"{filePath}にweapons配列が見つかりませんでした。"); }
        if (rootNode?["party"]?["_armors"] is not JsonObject heldArmorsJsonObject) { return new Err<SaveData>($"{filePath}にarmors配列が見つかりませんでした。"); }
        if (!(await systemLoader.LoadAsync()).Unwrap(out var system, out var message)) { return new Err<SaveData>(message); }
        if (!(await itemsLoader.LoadAsync()).Unwrap(out var items, out message)) { return new Err<SaveData>(message); }
        if (!(await weaponsLoader.LoadAsync()).Unwrap(out var weapons, out message)) { return new Err<SaveData>(message); }
        if (!(await armorsLoader.LoadAsync()).Unwrap(out var armors, out message)) { return new Err<SaveData>(message); }
        var switches = switchesJsonArray.Select(x => x?.GetValue<bool?>());
        var variables = variablesJsonArray.Select(
            x => x is not null
                ? x.GetValueKind() switch
                {
                    JsonValueKind.String => x.GetValue<string>(),
                    JsonValueKind.Number => x.GetValue<int>(),
                    JsonValueKind.True or JsonValueKind.False => x.GetValue<bool>(),
                    JsonValueKind.Null => null,
                    // いずれにも一致しない場合は元のJsonNodeを返す
                    _ => (object?)x,
                }
                : null
        );
        var actors = actorsJsonArray.Select(
            x => x is not null
                ? new ActorDataDto(
                    x?["_name"]?.GetValue<string>() ?? "",
                    x?["_hp"]?.GetValue<int>() ?? default,
                    x?["_mp"]?.GetValue<int>() ?? default,
                    x?["_tp"]?.GetValue<int>() ?? default,
                    x?["_level"]?.GetValue<int>() ?? default,
                    x?["_exp"]?["1"]?.GetValue<int>() ?? default
                )
                : null
        );
        var gold = goldJsonNode.GetValue<int>();
        List<HeldItemDataDto> heldItems = [];
        foreach (var pair in heldItemsJsonObject)
        {
            if (int.TryParse(pair.Key, out var id) && pair.Value?.GetValue<int>() is int count)
            {
                heldItems.Add(new(id, count));
            }
        }
        List<HeldWeaponDataDto> heldWeapons = [];
        foreach (var pair in heldWeaponsJsonObject)
        {
            if (int.TryParse(pair.Key, out var id) && pair.Value?.GetValue<int>() is int count)
            {
                heldWeapons.Add(new(id, count));
            }
        }
        List<HeldArmorDataDto> heldArmors = [];
        foreach (var pair in heldArmorsJsonObject)
        {
            if (int.TryParse(pair.Key, out var id) && pair.Value?.GetValue<int>() is int count)
            {
                heldArmors.Add(new(id, count));
            }
        }
        var dto = new SaveDataDataDto([.. switches], [.. variables], [.. actors], gold, [.. heldItems], [.. heldWeapons], [.. heldArmors]);
        return new Ok<SaveData>(dto.ToModel(system, items, weapons, armors));
    }

    public Task SaveAsync(SaveData saveData)
    {
        throw new NotImplementedException();
    }
}
