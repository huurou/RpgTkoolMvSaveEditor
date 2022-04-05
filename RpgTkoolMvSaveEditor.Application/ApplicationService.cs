using RpgTkoolMvSaveEditor.Domain;
using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Application;

public class ApplicationService
{
    public event EventHandler<string>? ErrorOcuured;

    private const string WWW_DIR = "www";

    private readonly IGameDataLoader gameDataLoader_;
    private readonly ICommonDataLoader commonDataLoader_;

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
        if (!Directory.Exists(dirPath))
        {
            ErrorOcuured?.Invoke(this, $"{dirPath}は存在しないかフォルダではありません。");
            return false;
        }
        var dirInfo = new DirectoryInfo(dirPath);
        Debug.WriteLine(dirInfo.Name);
        foreach (var item in dirInfo.EnumerateDirectories())
        {
            Debug.WriteLine(item.Name);
        }
        var wwwDir = dirInfo.Name == WWW_DIR ? dirPath
            : dirInfo.EnumerateDirectories().Any(x => x.Name == WWW_DIR) ? Path.Combine(dirPath, WWW_DIR)
            : null;
        if (wwwDir is null)
        {
            ErrorOcuured?.Invoke(this, "ゲームフォルダかwwwフォルダを指定してください。");
            return false;
        }

        LoadWwwDir(wwwDir);

        return true;
    }

    private void LoadWwwDir(string wwwDir)
    {
        var system = gameDataLoader_.Load<SystemData>(Path.Combine(wwwDir, "data", "System.json"));
        var common = commonDataLoader_.Load(Path.Combine(wwwDir, "save", "common.rpgsave"));
    }
}