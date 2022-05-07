using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using RpgTkoolMvSaveEditor.Domain.GameDatas;
using RpgTkoolMvSaveEditor.Domain.SaveDatas;

namespace RpgTkoolMvSaveEditor.Application;

public class ApplicationService
{
    public event EventHandler<string>? ErrorOccurred;
    public event EventHandler<string>? DataLoaded;
    public event EventHandler<(IEnumerable<GameSwitch> switches, IEnumerable<GameVariable> variables)>? CommonDataLoaded;
    public event EventHandler<(Parameters? parameters,
                               IEnumerable<Switch> switches,
                               IEnumerable<Variable> variables,
                               IEnumerable<Item> items,
                               IEnumerable<Weapon> weapons,
                               IEnumerable<Armor> armors,
                               IEnumerable<Actor> actors)>? SaveDataLoaded;

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;
    private readonly ISaveDataLoader saveDataLoader_;

    private readonly DataPathService dataPathService_ = new();
    private SystemData? systemData_;
    private List<ItemData?>? itemsData_;
    private List<WeaponData?>? weaponsData_;
    private List<ArmorData?>? armorsData_;
    private CommonData? commonData_;
    private SaveData? saveData_;

    public ApplicationService(IGameDataLoader gameDataLoader,
                              ICommonDataLoader commonDataLoader,
                              ISaveDataLoader saveDataLoader)
    {
        gameDataLoader_ = gameDataLoader;
        commonDataLoader_ = commonDataLoader;
        saveDataLoader_ = saveDataLoader;

        dataPathService_.ErrorOccurred += (s, e) => ErrorOccurred?.Invoke(s, e);
    }

    /// <summary>
    /// ゲームフォルダかwwwフォルダを読み込む
    /// </summary>
    /// <param name="dirPath">ゲームフォルダかwwwフォルダ</param>
    /// <returns>成功/失敗<para/>
    /// wwwフォルダでない、あるいは直下にwwwフォルダがない場合失敗
    /// wwwフォルダ直下に必要なフォルダ、ファイルがない場合失敗</returns>
    public async Task<bool> LoadDirectoryAsync(string dirPath)
    {
        if (!dataPathService_.SearchWwwDirectory(dirPath)) return false;
        await LoadDataAsync();
        return true;
    }

    public void SetCommonDataSwitch(string id, bool? value)
    {
        if (commonData_ is not null) commonData_.GameSwitches[id] = value;
    }

    public void SetCommonDataVariable(string id, object? value)
    {
        if (commonData_ is not null) commonData_.GameVariables[id] = value;
    }

    public void SetSaveDataGold(int gold)
    {
        if (saveData_ is not null) saveData_.Parameters.Gold = gold;
    }

    public void SetSaveDataSwitch(int id, bool? value)
    {
        if (saveData_ is not null) saveData_.Switches[id] = value;
    }

    public void SetSaveDataVariable(int id, object? value)
    {
        if (saveData_ is not null) saveData_.Variables[id] = value;
    }

    public void SetSaveDataItem(string id, int count)
    {
        if (saveData_ is not null) saveData_.Items[id] = count;
    }

    public void SetSaveDataWeapon(string id, int count)
    {
        if (saveData_ is not null) saveData_.Weapons[id] = count;
    }

    public void SetSaveDataArmor(string id, int count)
    {
        if (saveData_ is not null) saveData_.Armors[id] = count;
    }

    private async Task LoadDataAsync()
    {
        systemData_ = await gameDataLoader_.LoadAsync<SystemData>(dataPathService_.SystemDataPath);
        itemsData_ = await gameDataLoader_.LoadAsync<List<ItemData?>>(dataPathService_.ItemsDataPath);
        weaponsData_ = await gameDataLoader_.LoadAsync<List<WeaponData?>>(dataPathService_.WeaponsDataPath);
        armorsData_ = await gameDataLoader_.LoadAsync<List<ArmorData?>>(dataPathService_.ArmorsDataPath);
        commonData_ = await commonDataLoader_.LoadAsync(dataPathService_.CommonDataPath);
        saveData_ = await saveDataLoader_.LoadAsync(dataPathService_.SaveDataPath);

        DataLoaded?.Invoke(this, systemData_?.GameTitle ?? "");
        CommonDataLoaded?.Invoke(this, (GetGameSwitches(), GetGameVariables()));
        SaveDataLoaded?.Invoke(this, (GetParameters(), GetSwitches(), GetVariables(), GetItems(), GetWeapons(), GetArmors(), GetActors()));

        IEnumerable<GameSwitch> GetGameSwitches()
        {
            if (systemData_ is null || commonData_ is null) yield break;
            foreach (var sw in commonData_.GameSwitches)
            {
                if (!int.TryParse(sw.Key, out var index)) continue;
                if (string.IsNullOrEmpty(systemData_.Switches[index])) continue;
                yield return new(sw.Key, systemData_.Switches[index], sw.Value);
            }
        }

        IEnumerable<GameVariable> GetGameVariables()
        {
            if (systemData_ is null || commonData_ is null) yield break;
            foreach (var va in commonData_.GameVariables)
            {
                if (!int.TryParse(va.Key, out var index)) continue;
                if (string.IsNullOrEmpty(systemData_.Variables[index])) continue;
                yield return new(va.Key, systemData_.Variables[index], va.Value);
            }
        }

        Parameters? GetParameters() => saveData_ is null ? null : new Parameters(saveData_.Parameters);

        IEnumerable<Switch> GetSwitches()
        {
            if (systemData_ is null || saveData_ is null) yield break;
            for (var i = 0; i < systemData_.Switches.Count; i++)
            {
                if (string.IsNullOrEmpty(systemData_.Switches[i])) continue;
                yield return new(i, systemData_.Switches[i], i < saveData_.Switches.Count ? saveData_.Switches[i] : null);
            }
        }

        IEnumerable<Variable> GetVariables()
        {
            if (systemData_ is null || saveData_ is null) yield break;
            for (var i = 0; i < systemData_.Variables.Count; i++)
            {
                if (string.IsNullOrEmpty(systemData_.Variables[i])) continue;
                yield return new(i, systemData_.Variables[i], i < saveData_.Variables.Count ? saveData_.Variables[i] : null);
            }
        }

        IEnumerable<Item> GetItems()
        {
            if (itemsData_ is null || saveData_ is null) yield break;
            for (var i = 0; i < itemsData_.Count; i++)
            {
                if (string.IsNullOrEmpty(itemsData_[i]?.Name)) continue;
                if (!saveData_.Items.TryGetValue(i.ToString(), out var count)) count = 0;
                yield return new(i, itemsData_[i]?.Name ?? "", itemsData_[i]?.Description ?? "", count);
            }
        }

        IEnumerable<Weapon> GetWeapons()
        {
            if (weaponsData_ is null || saveData_ is null) yield break;
            for (var i = 0; i < weaponsData_.Count; i++)
            {
                if (string.IsNullOrEmpty(weaponsData_[i]?.Name)) continue;
                if (!saveData_.Weapons.TryGetValue(i.ToString(), out var count)) count = 0;
                yield return new(i, weaponsData_[i]?.Name ?? "", weaponsData_[i]?.Description ?? "", count);
            }
        }

        IEnumerable<Armor> GetArmors()
        {
            if (armorsData_ is null || saveData_ is null) yield break;
            for (var i = 0; i < armorsData_.Count; i++)
            {
                if (string.IsNullOrEmpty(armorsData_[i]?.Name)) continue;
                if (!saveData_.Armors.TryGetValue(i.ToString(), out var count)) count = 0;
                yield return new(i, armorsData_[i]?.Name ?? "", armorsData_[i]?.Description ?? "", count);
            }
        }
        IEnumerable<Actor> GetActors()
        {
            if (saveData_ is null) yield break;
            foreach (var actorData in saveData_.Actors)
            {
                if (actorData is null || string.IsNullOrEmpty(actorData.Name)) continue;
                yield return new(actorData);
            }
        }
    }
}