using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using RpgTkoolMvSaveEditor.Domain.GameDatas;

namespace RpgTkoolMvSaveEditor.Application;

public class ApplicationService
{
    public event EventHandler<string>? ErrorOcuured;

    private const string WWW_DIR = "www";

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;

    private SystemData? systemData_;
    private CommonData? commonData_;

    public ApplicationService(IGameDataLoader gameDataLoader,
                              ICommonDataLoader commonDataLoader)
    {
        gameDataLoader_ = gameDataLoader;
        commonDataLoader_ = commonDataLoader;
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
        if (!TryParseDirectory(dirPath, out var wwwDir)) return false;
        try
        {
            LoadWwwDir(wwwDir);
        }
        catch (Exception ex)
        {
            ErrorOcuured?.Invoke(this, ex.Message);
            return false;
        }
        return true;
    }

    private bool TryParseDirectory(string dirPath, out string wwwDir)
    {
        wwwDir = "";
        if (!Directory.Exists(dirPath))
        {
            ErrorOcuured?.Invoke(this, $"{dirPath}は存在しないかフォルダではありません。");
            return false;
        }
        var dirInfo = new DirectoryInfo(dirPath);
        wwwDir = dirInfo.Name == WWW_DIR ? dirPath
            : dirInfo.EnumerateDirectories().Any(x => x.Name == WWW_DIR) ? Path.Combine(dirPath, WWW_DIR)
            : "";
        if (string.IsNullOrEmpty(wwwDir))
        {
            ErrorOcuured?.Invoke(this, "ゲームフォルダかwwwフォルダを指定してください。");
            return false;
        }
        return true;
    }

    private void LoadWwwDir(string wwwDir)
    {
        systemData_ = gameDataLoader_.Load<SystemData>(Path.Combine(wwwDir, "data", "System.json"));
        commonData_ = commonDataLoader_.Load(Path.Combine(wwwDir, "save", "common.rpgsave"));
    }
}