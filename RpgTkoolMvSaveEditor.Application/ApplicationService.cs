using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using RpgTkoolMvSaveEditor.Domain.GameDatas;
using RpgTkoolMvSaveEditor.Domain.SaveDatas;

namespace RpgTkoolMvSaveEditor.Application;

public class ApplicationService
{
    public event EventHandler<string>? ErrorOccurred;
    public event EventHandler<(IEnumerable<GameSwitch> switches, IEnumerable<GameVariable> variables)>? CommonDataLoaded;
    public event EventHandler<(IEnumerable<Switch> switches,
                               IEnumerable<Variable> variables)>? SaveDataLoaded;

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;
    private readonly ISaveDataLoader saveDataLoader_;

    private readonly DataService dataService_ = new();
    private SystemData? systemData_;
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
        try
        {
        }
        catch (Exception ex)
        {
            ErrorOccurred?.Invoke(this, ex.Message);
            return false;
        }
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

    private void LoadData()
    {
        if (File.Exists(dataService_.SystemDataPath)) systemData_ = gameDataLoader_.Load<SystemData>(dataService_.SystemDataPath);
        if (File.Exists(dataService_.CommonDataPath)) commonData_ = commonDataLoader_.Load(dataService_.CommonDataPath);
        if (File.Exists(dataService_.SaveDataPath)) saveData_ = saveDataLoader_.Load(dataService_.SaveDataPath);

        CommonDataLoaded?.Invoke(this, (GetGameSwitches(), GetGameVariables()));
        SaveDataLoaded?.Invoke(this, (GetSwitches(), GetVariables()));

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
                if (i >= saveData_.Switches.Count) continue;
                yield return new(i, systemData_.Switches[i], saveData_.Switches[i]);
            }
        }

        IEnumerable<Variable> GetVariables()
        {
            if (systemData_ is null || saveData_ is null) yield break;
            for (var i = 0; i < systemData_.Variables.Count; i++)
            {
                if (string.IsNullOrEmpty(systemData_.Variables[i])) continue;
                if (i >= saveData_.Variables.Count) continue;
                yield return new(i, systemData_.Variables[i], saveData_.Variables[i]);
            }
        }
    }
}