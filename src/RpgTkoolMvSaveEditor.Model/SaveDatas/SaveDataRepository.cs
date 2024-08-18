using LZStringCSharp;
using Microsoft.Extensions.Options;
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
        if (rootNode is null) { return new Err<SaveData>($"{filePath}のパースに失敗しました。"); }
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

    public async Task<Result> SaveAsync(SaveData saveData)
    {
        var dto = SaveDataDataDto.FromModel(saveData);
        var filePath = Path.Combine(wwwContext.WwwDirPath, "save", "file1.rpgsave");
        if (!File.Exists(filePath)) { return new Err($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        var rootNode = await JsonNode.ParseAsync(memoryStream);
        if (rootNode is null) { return new Err($"{filePath}のパースに失敗しました。"); }
        if (rootNode?["switches"]?["_data"]?["@a"] is not JsonArray switchesJsonArray) { return new Err($"{filePath}にswitches配列が見つかりませんでした。"); }
        if (rootNode?["variables"]?["_data"]?["@a"] is not JsonArray variablesJsonArray) { return new Err($"{filePath}にvariables配列が見つかりませんでした。"); }
        if (rootNode?["actors"]?["_data"]?["@a"] is not JsonArray actorsJsonArray) { return new Err($"{filePath}にactors配列が見つかりませんでした。"); }
        if (rootNode?["party"]?["_gold"] is not JsonNode goldJsonNode) { return new Err($"{filePath}にgold要素が見つかりませんでした。"); }
        if (rootNode?["party"]?["_items"] is not JsonObject heldItemsJsonObject) { return new Err($"{filePath}にitems配列が見つかりませんでした。"); }
        if (rootNode?["party"]?["_weapons"] is not JsonObject heldWeaponsJsonObject) { return new Err($"{filePath}にweapons配列が見つかりませんでした。"); }
        if (rootNode?["party"]?["_armors"] is not JsonObject heldArmorsJsonObject) { return new Err($"{filePath}にarmors配列が見つかりませんでした。"); }
        for (var i = 0; i < dto.Switches.Count; i++)
        {
            if (i >= switchesJsonArray.Count)
            {
                switchesJsonArray.Add(null);
            }
            switchesJsonArray[i] = dto.Switches[i];
        }
        for (var i = 0; i < dto.Variables.Count; i++)
        {
            if (i >= variablesJsonArray.Count)
            {
                variablesJsonArray.Add(null);
            }
            variablesJsonArray[i] = JsonValue.Create(dto.Variables[i]);
        }
        for (var i = 0; i < dto.Actors.Count; i++)
        {
            if (actorsJsonArray[i] is JsonObject actorJsonObject && dto.Actors[i] is ActorDataDto actor)
            {
                actorJsonObject["_name"] = actor.Name;
                actorJsonObject["_hp"] = actor.HP;
                actorJsonObject["_mp"] = actor.MP;
                actorJsonObject["_tp"] = actor.TP;
                actorJsonObject["_level"] = actor.Level;
                actorJsonObject["_exp"]!["1"] = actor.Exp;
            }
        }
        goldJsonNode.AsValue().ReplaceWith(dto.Gold);
        for(var i = 0; i < dto.HeldItems.Count; i++)
        {
            heldItemsJsonObject[dto.HeldItems[i].Id.ToString()] = dto.HeldItems[i].Count;
        }
        for(var i = 0; i < dto.HeldWeapons.Count; i++)
        {
            heldWeaponsJsonObject[dto.HeldWeapons[i].Id.ToString()] = dto.HeldWeapons[i].Count;
        }
        for(var i = 0; i < dto.HeldArmors.Count; i++)
        {
            heldArmorsJsonObject[dto.HeldArmors[i].Id.ToString()] = dto.HeldArmors[i].Count;
        }
        await File.WriteAllTextAsync(filePath, LZString.CompressToBase64(JsonSerializer.Serialize(rootNode)));
        return new Ok();
    }
}
