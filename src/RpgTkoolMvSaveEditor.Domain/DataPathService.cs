namespace RpgTkoolMvSaveEditor.Domain;

public class DataPathService
{
    public event EventHandler<string>? ErrorOccurred;

    private const string WWW_DIR_NAME = "www";
    private const string DATA_DIR_NAME = "data";
    private const string SAVE_DIR_NAME = "save";
    private const string SYSTEM_JSON_NAME = "System.json";
    private const string ITEMS_JSON_NAME = "Items.json";
    private const string WEAPONS_JSON_NAME = "Weapons.json";
    private const string ARMORS_JSON_NAME = "Armors.json";
    private const string COMMON_RPGSAVE_NAME = "common.rpgsave";
    private const string SAVE_RPGSAVE_NAME = "file1.rpgsave";

    private string? wwwDirPath_;

    public string SystemDataPath => Path.Combine(wwwDirPath_ ?? "", DATA_DIR_NAME, SYSTEM_JSON_NAME);
    public string ItemsDataPath => Path.Combine(wwwDirPath_ ?? "", DATA_DIR_NAME, ITEMS_JSON_NAME);
    public string WeaponsDataPath => Path.Combine(wwwDirPath_ ?? "", DATA_DIR_NAME, WEAPONS_JSON_NAME);
    public string ArmorsDataPath => Path.Combine(wwwDirPath_ ?? "", DATA_DIR_NAME, ARMORS_JSON_NAME);
    public string SaveDirPath => Path.Combine(wwwDirPath_ ?? "", SAVE_DIR_NAME);
    public string CommonDataPath => Path.Combine(SaveDirPath, COMMON_RPGSAVE_NAME);
    public string SaveDataPath => Path.Combine(SaveDirPath, SAVE_RPGSAVE_NAME);

    public bool SearchWwwDirectory(string dirPath)
    {
        if (!Directory.Exists(dirPath))
        {
            ErrorOccurred?.Invoke(this, $"{dirPath}は存在しないかフォルダではありません。");
            return false;
        }
        var dirInfo = new DirectoryInfo(dirPath);
        wwwDirPath_ = dirInfo.Name == WWW_DIR_NAME ? dirPath
            : dirInfo.EnumerateDirectories().Any(x => x.Name == WWW_DIR_NAME) ? Path.Combine(dirPath, WWW_DIR_NAME)
            : null;
        if (string.IsNullOrEmpty(wwwDirPath_))
        {
            ErrorOccurred?.Invoke(this, "ゲームフォルダかwwwフォルダを指定してください。");
            return false;
        }
        return true;
    }
}