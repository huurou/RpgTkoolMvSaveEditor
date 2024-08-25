using RpgTkoolMvSaveEditor.Model.GameData.Actors;
using RpgTkoolMvSaveEditor.Model.GameData.Armors;
using RpgTkoolMvSaveEditor.Model.GameData.Items;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.Switches;
using RpgTkoolMvSaveEditor.Model.GameData.Variables;
using RpgTkoolMvSaveEditor.Model.GameData.Weapons;
using RpgTkoolMvSaveEditor.Model.Queries.Common;
using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model.Queries;

public record GetSaveDataQuery() : IQuery;

public class GetSaveDataQueryHandler(Context context, SaveDataJsonNodeStore saveDataJsonNodeStore, SystemDataLoader systemDataLoader) : IQueryHandler<GetSaveDataQuery, SaveDataViewDto>
{
    private IEnumerable<ItemDataDto>? items_;
    private IEnumerable<WeaponDataDto>? weapons_;
    private IEnumerable<ArmorDataDto>? armors_;
    private readonly JsonSerializerOptions options_ = new(JsonSerializerDefaults.Web);

    public async Task<Result<SaveDataViewDto>> HandleAsync(GetSaveDataQuery query)
    {
        if (context.WwwDirPath is null) { return new Err<SaveDataViewDto>("wwwフォルダが選択されていません。"); }
        if (!(await saveDataJsonNodeStore.LoadAsync(context.WwwDirPath)).Unwrap(out var rootNode, out var message)) { return new Err<SaveDataViewDto>(message); }
        if (rootNode["switches"]?["_data"]?["@a"] is not JsonArray switchValuesJsonArray) { return new Err<SaveDataViewDto>("セーブデータにswitches::_data::@aが見つかりませんでした。"); }
        if (rootNode["variables"]?["_data"]?["@a"] is not JsonArray variableValuesJsonArray) { return new Err<SaveDataViewDto>("セーブデータにvariables::_data::@aが見つかりませんでした。"); }
        if (rootNode["actors"]?["_data"]?["@a"] is not JsonArray actorsJsonArray) { return new Err<SaveDataViewDto>("セーブデータにactors::_data::@aが見つかりませんでした。"); }
        if (rootNode["party"]?["_gold"] is not JsonNode goldJsonNode) { return new Err<SaveDataViewDto>("セーブデータにparty::_goldが見つかりませんでした。"); }
        if (rootNode["party"]?["_items"] is not JsonObject heldItemsJsonObject) { return new Err<SaveDataViewDto>("セーブデータにparty::_itemsが見つかりませんでした。"); }
        if (rootNode["party"]?["_weapons"] is not JsonObject heldWeaponsJsonObject) { return new Err<SaveDataViewDto>("セーブデータにparty::_weaponsが見つかりませんでした。"); }
        if (rootNode["party"]?["_armors"] is not JsonObject heldArmorsJsonObject) { return new Err<SaveDataViewDto>("セーブデータにparty::_armorsが見つかりませんでした。"); }
        if (!(await systemDataLoader.LoadAsync()).Unwrap(out var systemData, out message)) { return new Err<SaveDataViewDto>(message); }
        if (!(await LoadItemsAsync(context.WwwDirPath)).Unwrap(out var items, out message)) { return new Err<SaveDataViewDto>(message); }
        if (!(await LoadWeaponsAsync(context.WwwDirPath)).Unwrap(out var weapons, out message)) { return new Err<SaveDataViewDto>(message); }
        if (!(await LoadArmorsAsync(context.WwwDirPath)).Unwrap(out var armors, out message)) { return new Err<SaveDataViewDto>(message); }
        var switchValues = switchValuesJsonArray.Select(y => y?.GetValue<bool?>()).ToList();
        var switches = systemData.Switches.Select((x, i) => (Id: i, Name: x)).Skip(1).Select(x => new SwitchViewDto(x.Id, x.Name, switchValues[x.Id]));
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
        var variables = systemData.Variables.Select((x, i) => (Id: i, Name: x)).Skip(1).Select(x => new VariableViewDto(x.Id, x.Name, variableValues[x.Id]));
        var actors = actorsJsonArray.Select((x, i) => (Id: i, Value: x)).Skip(1).Select(
            x => x.Value is not null
                ? new ActorViewDto(
                    x.Id,
                    x.Value["_name"]?.GetValue<string>() ?? "",
                    x.Value["_hp"]?.GetValue<int>() ?? default,
                    x.Value["_mp"]?.GetValue<int>() ?? default,
                    x.Value["_tp"]?.GetValue<int>() ?? default,
                    x.Value["_level"]?.GetValue<int>() ?? default,
                    x.Value["_exp"]?["1"]?.GetValue<int>() ?? default
                )
                : null
        );
        var gold = goldJsonNode.GetValue<int>();
        var heldItems = items.Select((x, i) => (Id: i, Item: x)).Skip(1).Select(
            x => new HeldItemViewDto(
                x.Item.Id, x.Item.Name, x.Item.Description,
                heldItemsJsonObject.TryGetPropertyValue(x.Id.ToString(), out var countNode) && countNode is not null ? countNode.GetValue<int>() : 0
            )
        );
        var heldWeapons = weapons.Select((x, i) => (Id: i, Weapon: x)).Skip(1).Select(
            x => new HeldWeaponViewDto(
                x.Weapon.Id, x.Weapon.Name, x.Weapon.Description,
                heldWeaponsJsonObject.TryGetPropertyValue(x.Id.ToString(), out var countNode) && countNode is not null ? countNode.GetValue<int>() : 0
            )
        );
        var heldArmors = armors.Select((x, i) => (Id: i, Armor: x)).Skip(1).Select(
            x => new HeldArmorViewDto(
                x.Armor.Id, x.Armor.Name, x.Armor.Description,
                heldArmorsJsonObject.TryGetPropertyValue(x.Id.ToString(), out var countNode) && countNode is not null ? countNode.GetValue<int>() : 0
            )
        );
        return new Ok<SaveDataViewDto>(new([.. switches], [.. variables], gold, [.. actors], [.. heldItems], [.. heldWeapons], [.. heldArmors]));
    }

    private async Task<Result<IEnumerable<ItemDataDto>>> LoadItemsAsync(string wwwDirPath)
    {
        if (items_ is not null) { return new Ok<IEnumerable<ItemDataDto>>(items_); }
        var filePath = Path.Combine(wwwDirPath, "data", "Items.json");
        if (!File.Exists(filePath)) { return new Err<IEnumerable<ItemDataDto>>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        items_ = await JsonSerializer.DeserializeAsync<IEnumerable<ItemDataDto>>(fileStream, options_);
        return items_ is not null
            ? new Ok<IEnumerable<ItemDataDto>>(items_)
            : new Err<IEnumerable<ItemDataDto>>($"{filePath}のロードに失敗しました。");
    }

    private async Task<Result<IEnumerable<WeaponDataDto>>> LoadWeaponsAsync(string wwwDirPath)
    {
        if (weapons_ is not null) { return new Ok<IEnumerable<WeaponDataDto>>(weapons_); }
        var filePath = Path.Combine(wwwDirPath, "data", "Weapons.json");
        if (!File.Exists(filePath)) { return new Err<IEnumerable<WeaponDataDto>>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        weapons_ = await JsonSerializer.DeserializeAsync<IEnumerable<WeaponDataDto>>(fileStream, options_);
        return weapons_ is not null
            ? new Ok<IEnumerable<WeaponDataDto>>(weapons_)
            : new Err<IEnumerable<WeaponDataDto>>($"{filePath}のロードに失敗しました。");
    }

    private async Task<Result<IEnumerable<ArmorDataDto>>> LoadArmorsAsync(string wwwDirPath)
    {
        if (armors_ is not null) { return new Ok<IEnumerable<ArmorDataDto>>(armors_); }
        var filePath = Path.Combine(wwwDirPath, "data", "Armors.json");
        if (!File.Exists(filePath)) { return new Err<IEnumerable<ArmorDataDto>>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        armors_ = await JsonSerializer.DeserializeAsync<IEnumerable<ArmorDataDto>>(fileStream, options_);
        return armors_ is not null
            ? new Ok<IEnumerable<ArmorDataDto>>(armors_)
            : new Err<IEnumerable<ArmorDataDto>>($"{filePath}のロードに失敗しました。");
    }
}
