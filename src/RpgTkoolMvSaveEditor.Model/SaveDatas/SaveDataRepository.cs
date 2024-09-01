using LZStringCSharp;
using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.GameData;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public class SaveDataRepository(PathProvider pathProvider, SaveDataJsonObjectProvider saveDataJsonObjectProvider, ILogger<SaveDataRepository> logger) : ISaveDataRepository
{
    public async Task<Result<SaveData>> LoadAsync()
    {
        logger.LogDebug("セーブデータをロードしています。");
        if (pathProvider.WwwDirPath is null) { return new Err<SaveData>("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(pathProvider.WwwDirPath, "save", "file1.rpgsave");
        if (!File.Exists(filePath)) { return new Err<SaveData>($"{filePath}が存在しません。"); }
        var json = LZString.DecompressFromBase64(await File.ReadAllTextAsync(filePath));
        using var jsonMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        if (!(await saveDataJsonObjectProvider.GetAsync()).Unwrap(out var rootObject, out var message)) { return new Err<SaveData>(message); }
        if (rootObject is null) { return new Err<SaveData>($"{filePath}のパースに失敗しました。"); }
        if (rootObject["switches"]?["_data"]?["@a"] is not JsonArray switchValuesJsonArray) { return new Err<SaveData>("セーブデータにswitches::_data::@aが見つかりませんでした。"); }
        if (rootObject["variables"]?["_data"]?["@a"] is not JsonArray variableValuesJsonArray) { return new Err<SaveData>("セーブデータにvariables::_data::@aが見つかりませんでした。"); }
        if (rootObject["actors"]?["_data"]?["@a"] is not JsonArray actorsJsonArray) { return new Err<SaveData>("セーブデータにactors::_data::@aが見つかりませんでした。"); }
        if (rootObject["party"]?["_gold"] is not JsonValue goldJsonValue) { return new Err<SaveData>("セーブデータにparty::_goldが見つかりませんでした。"); }
        if (rootObject["party"]?["_items"] is not JsonObject heldItemsJsonObject) { return new Err<SaveData>("セーブデータにparty::_itemsが見つかりませんでした。"); }
        if (rootObject["party"]?["_weapons"] is not JsonObject heldWeaponsJsonObject) { return new Err<SaveData>("セーブデータにparty::_weaponsが見つかりませんでした。"); }
        if (rootObject["party"]?["_armors"] is not JsonObject heldArmorsJsonObject) { return new Err<SaveData>("セーブデータにparty::_armorsが見つかりませんでした。"); }
        var systemFilePath = Path.Combine(pathProvider.WwwDirPath, "data", "System.json");
        if (!File.Exists(systemFilePath)) { return new Err<SaveData>($"{systemFilePath}が存在しません。"); }
        using var systemFileStream = new FileStream(systemFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var systemJsonObject = await JsonSerializer.DeserializeAsync<JsonObject>(systemFileStream);
        if (systemJsonObject is null) { return new Err<SaveData>($"{systemFilePath}のロードに失敗しました。"); }
        var switchNamesJsonArray = systemJsonObject["switches"]!.AsArray();
        var variableNamesJsonArray = systemJsonObject["variables"]!.AsArray();
        var itemsFilePath = Path.Combine(pathProvider.WwwDirPath, "data", "Items.json");
        if (!File.Exists(itemsFilePath)) { return new Err<SaveData>($"{itemsFilePath}が存在しません。"); }
        using var itemsFileStream = new FileStream(itemsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var itemDataJsonArray = await JsonSerializer.DeserializeAsync<JsonArray>(itemsFileStream);
        if (itemDataJsonArray is null) { return new Err<SaveData>($"{itemsFilePath}のロードに失敗しました。"); }
        var weaponsFilePath = Path.Combine(pathProvider.WwwDirPath, "data", "Weapons.json");
        if (!File.Exists(weaponsFilePath)) { return new Err<SaveData>($"{weaponsFilePath}が存在しません。"); }
        using var weaponsFileStream = new FileStream(weaponsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var weaponDataJsonArray = await JsonSerializer.DeserializeAsync<JsonArray>(weaponsFileStream);
        if (weaponDataJsonArray is null) { return new Err<SaveData>($"{weaponsFilePath}のロードに失敗しました。"); }
        var armorsFilePath = Path.Combine(pathProvider.WwwDirPath, "data", "Armors.json");
        if (!File.Exists(armorsFilePath)) { return new Err<SaveData>($"{armorsFilePath}が存在しません。"); }
        using var armorsFileStream = new FileStream(armorsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var armorDataJsonArray = await JsonSerializer.DeserializeAsync<JsonArray>(armorsFileStream);
        if (armorDataJsonArray is null) { return new Err<SaveData>($"{armorsFilePath}のロードに失敗しました。"); }
        var switches = switchNamesJsonArray
            .Select((x, i) => (Id: i, Name: x!.GetValue<string>())).Skip(1)
            .Select(x => new Switch(x.Id, x.Name, x.Id < switchValuesJsonArray.Count ? switchValuesJsonArray[x.Id]?.GetValue<bool?>() : null));
        var variableValues = variableValuesJsonArray.Select(
            x => x?.GetValueKind() switch
            {
                JsonValueKind.String => x.GetValue<string>(),
                JsonValueKind.Number => x.GetValue<int>(),
                JsonValueKind.True or JsonValueKind.False => x.GetValue<bool>(),
                JsonValueKind.Null => null,
                // いずれにも一致しない場合は元のJsonNodeを返す
                _ => (object?)x,
            }
        ).ToList();
        var variables = variableNamesJsonArray
            .Select((x, i) => (Id: i, Name: x!.GetValue<string>())).Skip(1)
            .Select(x => new Variable(x.Id, x.Name, x.Id < variableValues.Count ? variableValues[x.Id] : null));
        var gold = goldJsonValue!.GetValue<int>();
        var actors = actorsJsonArray.Select((x, i) => (Id: i, Value: x)).Where(x => x.Value is not null).Select(
            x => new Actor(
                x.Id,
                x.Value?["_name"]?.GetValue<string>() ?? "",
                x.Value?["_hp"]?.GetValue<int>() ?? default,
                x.Value?["_mp"]?.GetValue<int>() ?? default,
                x.Value?["_tp"]?.GetValue<int>() ?? default,
                x.Value?["_level"]?.GetValue<int>() ?? default,
                x.Value?["_exp"]?["1"]?.GetValue<int>() ?? default
            )
        );
        var items = itemDataJsonArray.OfType<JsonNode>().Select(
            x => new Item(
                x!["id"]!.GetValue<int>(),
                x["name"]!.GetValue<string>(),
                x["description"]!.GetValue<string>(),
                heldItemsJsonObject.TryGetPropertyValue(x["id"]!.GetValue<int>().ToString(), out var countJsonNode) ? countJsonNode!.GetValue<int>() : 0
            )
        );
        var weapons = weaponDataJsonArray.OfType<JsonNode>().Select(
            x => new Weapon(
                x!["id"]!.GetValue<int>(),
                x["name"]!.GetValue<string>(),
                x["description"]!.GetValue<string>(),
                heldWeaponsJsonObject.TryGetPropertyValue(x["id"]!.GetValue<int>().ToString(), out var countJsonNode) ? countJsonNode!.GetValue<int>() : 0
            )
        );
        var armors = armorDataJsonArray.OfType<JsonNode>().Select(
            x => new Armor(
                x!["id"]!.GetValue<int>(),
                x["name"]!.GetValue<string>(),
                x["description"]!.GetValue<string>(),
                heldArmorsJsonObject.TryGetPropertyValue(x["id"]!.GetValue<int>().ToString(), out var countJsonNode) ? countJsonNode!.GetValue<int>() : 0
            )
        );
        var saveData = new SaveData([.. switches], [.. variables], gold, [.. actors], [.. items], [.. weapons], [.. armors]);
        logger.LogInformation("セーブデータがロードされました。");
        return new Ok<SaveData>(saveData);
    }

    public async Task<Result> SaveAsync(SaveData saveData)
    {
        logger.LogDebug("セーブデータをセーブしています。");
        if (pathProvider.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        var filePath = Path.Combine(pathProvider.WwwDirPath, "save", "file1.rpgsave");
        if (!File.Exists(filePath)) { return new Err($"{filePath}が存在しません。"); }
        if (!(await saveDataJsonObjectProvider.GetAsync()).Unwrap(out var rootObject, out var message)) { return new Err(message); }
        if (rootObject["switches"]?["_data"]?["@a"] is not JsonArray switchValuesJsonArray) { return new Err("セーブデータにswitches::_data::@aが見つかりませんでした。"); }
        if (rootObject["variables"]?["_data"]?["@a"] is not JsonArray variableValuesJsonArray) { return new Err("セーブデータにvariables::_data::@aが見つかりませんでした。"); }
        if (rootObject["actors"]?["_data"]?["@a"] is not JsonArray actorsJsonArray) { return new Err("セーブデータにactors::_data::@aが見つかりませんでした。"); }
        if (rootObject["party"]?["_gold"] is not JsonValue goldJsonValue) { return new Err("セーブデータにparty::_goldが見つかりませんでした。"); }
        if (rootObject["party"]?["_items"] is not JsonObject heldItemsJsonObject) { return new Err("セーブデータにparty::_itemsが見つかりませんでした。"); }
        if (rootObject["party"]?["_weapons"] is not JsonObject heldWeaponsJsonObject) { return new Err("セーブデータにparty::_weaponsが見つかりませんでした。"); }
        if (rootObject["party"]?["_armors"] is not JsonObject heldArmorsJsonObject) { return new Err("セーブデータにparty::_armorsが見つかりませんでした。"); }
        foreach (var @switch in saveData.Switches)
        {
            // セーブデータのスイッチ配列は要素数が全スイッチ数より少ないことがあるので足りない分だけ増やす
            while (@switch.Id >= switchValuesJsonArray.Count)
            {
                switchValuesJsonArray.Add(null);
            }
            switchValuesJsonArray[@switch.Id] = @switch.Value;
        }
        foreach (var variable in saveData.Variables)
        {
            // セーブデータの変数配列は要素数が全変数数より少ないことがあるので足りない分だけ増やす
            while (variable.Id >= variableValuesJsonArray.Count)
            {
                variableValuesJsonArray.Add(null);
            }
            variableValuesJsonArray[variable.Id] = JsonValue.Create(variable.Value);
        }
        goldJsonValue.ReplaceWith(saveData.Gold);
        foreach (var actor in saveData.Actors)
        {
            var actorJsonObject = actorsJsonArray[actor.Id]?.AsObject();
            if (actorJsonObject is null) { return new Err($"指定Id:{actor.Id}のアクターが存在しません。"); }
            actorJsonObject["_name"] = actor.Name;
            actorJsonObject["_hp"] = actor.HP;
            actorJsonObject["_mp"] = actor.MP;
            actorJsonObject["_tp"] = actor.TP;
            actorJsonObject["_level"] = actor.Level;
            actorJsonObject["_exp"]!["1"] = actor.Exp;
        }
        foreach (var item in saveData.Items)
        {
            heldItemsJsonObject[item.Id.ToString()] = item.Count;
        }
        foreach (var weapon in saveData.Weapons)
        {
            heldWeaponsJsonObject[weapon.Id.ToString()] = weapon.Count;
        }
        foreach (var armor in saveData.Armors)
        {
            heldArmorsJsonObject[armor.Id.ToString()] = armor.Count;
        }
        using var jsonMemoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(jsonMemoryStream, rootObject);
        jsonMemoryStream.Position = 0;
        using var jsonMemoryStreamReader = new StreamReader(jsonMemoryStream);
        var json = await jsonMemoryStreamReader.ReadToEndAsync();
        await File.WriteAllTextAsync(filePath, LZString.CompressToBase64(json));
        logger.LogInformation("セーブデータがセーブされました。");
        return new Ok();
    }
}
