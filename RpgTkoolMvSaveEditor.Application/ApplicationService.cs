using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using RpgTkoolMvSaveEditor.Domain.GameDatas;
using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Application;

public class Switch
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public bool Value { get; init; }
}

public class Variable
{
    public int Id { get; }
    public string Name { get; } = "";
    public int Value { get; }
}

public class ApplicationService
{
    public event EventHandler<string>? ErrorOccurred;
    public event EventHandler<(IEnumerable<(int id, string name, bool value)> switches,
                               IEnumerable<(int id, string name, int value)> variables)>? CommonDataLoaded;

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;

    private readonly DataService dataService_ = new();
    private SystemData? systemData_;
    private CommonData? commonData_;

    public ApplicationService(IGameDataLoader gameDataLoader,
                              ICommonDataLoader commonDataLoader)
    {
        gameDataLoader_ = gameDataLoader;
        commonDataLoader_ = commonDataLoader;

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

    public void SetCommonDataSwitch(int id, bool value)
    {
        if (commonData_ is not null) commonData_.GameSwitches[id] = value;
    }

    public void SetCommonDataVariable(int id, int value)
    {
        if (commonData_ is not null) commonData_.GameVariables[id] = value;
    }

    private void LoadData()
    {
        if (File.Exists(dataService_.SystemDataPath)) systemData_ = gameDataLoader_.Load<SystemData>(dataService_.SystemDataPath);
        if (File.Exists(dataService_.CommonDataPath)) commonData_ = commonDataLoader_.Load(dataService_.CommonDataPath);

        NotifyCommonData();
    }

    private void NotifyCommonData()
    {
        if (systemData_ is null || commonData_ is null) return;
        foreach (var x in commonData_.GameSwitches)
        {
            Debug.WriteLine($"id:{x.Key} name:{systemData_.Switches[x.Key]} value:{x.Value}");
        }
        var switches = commonData_.GameSwitches.Select(x => (x.Key, systemData_.Switches[x.Key], x.Value));
        var variables = commonData_.GameVariables.Select(x => (x.Key, systemData_.Variables[x.Key], x.Value));
        foreach (var item in variables)
        {
            Debug.WriteLine(item);
        }
        CommonDataLoaded?.Invoke(this, (switches, variables));
    }
}