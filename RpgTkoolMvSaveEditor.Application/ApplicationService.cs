using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using RpgTkoolMvSaveEditor.Domain.GameDatas;

namespace RpgTkoolMvSaveEditor.Application;

public class ApplicationService
{
    public event EventHandler<string>? ErrorOccurred;
    public event EventHandler<(SystemData, CommonData)>? CommonDataLoaded;

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;

    private DataService dataService_ = new();
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
        try
        {
            LoadData();
        }
        catch (Exception ex)
        {
            ErrorOccurred?.Invoke(this, ex.Message);
            return false;
        }
        return true;
    }

    public void SetSwitch(int id, bool value)
    {
        if (commonData_ is not null) commonData_.GameSwitches[id] = value;
    }

    public void SetVariable(int id, int value)
    {
        if (commonData_ is not null) commonData_.GameVariables[id] = value;
    }

    private void LoadData()
    {
        if (File.Exists(dataService_.SystemDataPath)) systemData_ = gameDataLoader_.Load<SystemData>(dataService_.SystemDataPath);
        if (File.Exists(dataService_.CommonDataPath)) commonData_ = commonDataLoader_.Load(dataService_.CommonDataPath);
    }
}