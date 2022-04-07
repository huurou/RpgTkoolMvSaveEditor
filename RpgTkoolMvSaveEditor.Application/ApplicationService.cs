using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using RpgTkoolMvSaveEditor.Domain.GameDatas;
using RpgTkoolMvSaveEditor.Domain.SaveDatas;

namespace RpgTkoolMvSaveEditor.Application;

public class ApplicationService
{
    public event EventHandler<string>? ErrorOccurred;
    public event EventHandler<(IEnumerable<Switch> switches, IEnumerable<Variable> variables)>? CommonDataLoaded;

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;
    private readonly ISaveDataLoader saveDataLoader_;

    private readonly DataService dataService_ = new();
    private SystemData? systemData_;
    private CommonData? commonData_;
    private List<SaveData>? saveDataList_;

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

    private void LoadData()
    {
        if (File.Exists(dataService_.SystemDataPath)) systemData_ = gameDataLoader_.Load<SystemData>(dataService_.SystemDataPath);
        if (File.Exists(dataService_.CommonDataPath)) commonData_ = commonDataLoader_.Load(dataService_.CommonDataPath);
        saveDataList_ = dataService_.SaveDataPathes.Where(File.Exists).Select(saveDataLoader_.Load).ToList();

        CommonDataLoaded?.Invoke(this, (GetGameSwitches(), GetGameVariables()));

        IEnumerable<Switch> GetGameSwitches()
        {
            if (systemData_ is null || commonData_ is null) yield break;
            foreach (var sw in commonData_.GameSwitches)
            {
                if (!int.TryParse(sw.Key, out var index)) continue;
                if (string.IsNullOrEmpty(systemData_.Switches[index])) continue;
                yield return new(sw.Key, systemData_.Switches[index], sw.Value);
            }
        }

        IEnumerable<Variable> GetGameVariables()
        {
            if (systemData_ is null || commonData_ is null) yield break;
            foreach (var va in commonData_.GameVariables)
            {
                if (!int.TryParse(va.Key, out var index)) continue;
                if (string.IsNullOrEmpty(systemData_.Variables[index])) continue;
                yield return new(va.Key, systemData_.Variables[index], va.Value);
            }
        }
    }
}