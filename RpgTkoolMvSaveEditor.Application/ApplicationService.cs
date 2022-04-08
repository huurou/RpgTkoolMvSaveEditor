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
    public event EventHandler<(IEnumerable<Switch> switches,
                               IEnumerable<Variable> variables,
                               IEnumerable<Item> items,
                               IEnumerable<Armor> armors)>? SaveDataLoaded;

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;
    private readonly ISaveDataLoader saveDataLoader_;

    private readonly DataPathService dataService_ = new();
    private SystemData? systemData_;
    private List<ItemData?>? itemsData_;
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

        dataService_.ErrorOccurred += (s, e) => ErrorOccurred?.Invoke(s, e);
    }

    /// <summary>
    /// ゲームフォルダかwwwフォルダを読み込む
    /// </summary>
    /// <param name="dirPath">ゲームフォルダかwwwフォルダ</param>
    /// <returns>成功/失敗<para/>
    /// wwwフォルダでない、あるいは直下にwwwフォルダがない場合失敗
    /// wwwフォルダ直下に必要なフォルダ、ファイルがない場合失敗</returns>
    public bool LoadDirectory(string dirPath)
    {
        if (!dataService_.SearchWwwDirectory(dirPath)) return false;
        LoadData();
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

    public void SetSaveDataArmor(string id, int count)
    {
        if (saveData_ is not null) saveData_.Armors[id] = count;
    }

    private void LoadData()
    {
        systemData_ = gameDataLoader_.Load<SystemData>(dataService_.SystemDataPath);
        itemsData_ = gameDataLoader_.Load<List<ItemData?>>(dataService_.ItemsDataPath);
        armorsData_ = gameDataLoader_.Load<List<ArmorData?>>(dataService_.ArmorsDataPath);
        commonData_ = commonDataLoader_.Load(dataService_.CommonDataPath);
        saveData_ = saveDataLoader_.Load(dataService_.SaveDataPath);

        DataLoaded?.Invoke(this, systemData_?.GameTitle ?? "");
        CommonDataLoaded?.Invoke(this, (GetGameSwitches(), GetGameVariables()));
        SaveDataLoaded?.Invoke(this, (GetSwitches(), GetVariables(), GetItems(), GetArmors()));

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
    }
}