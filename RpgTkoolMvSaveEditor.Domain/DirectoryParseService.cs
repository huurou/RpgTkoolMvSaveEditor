namespace RpgTkoolMvSaveEditor.Domain;

public class DataService
{
    public event EventHandler<string>? ErrorOccurred;

    private const string WWW_DIR_NAME = "www";
    private const string DATA_DIR_NAME = "data";
    private const string SAVE_DIR_NAME = "save";
    private const string SYSTEM_JSON_NAME = "System.json";
    private const string COMMON_RPGSAVE_NAME = "common.rpgsave";
    private const string SAVE_RPGSAVE_NAME = "file*.rpgsave";

    private string? wwwDirPath_;

    public string SystemDataPath => string.IsNullOrEmpty(wwwDirPath_) ? "" : Path.Combine(wwwDirPath_, DATA_DIR_NAME, SYSTEM_JSON_NAME);
    public string CommonDataPath => string.IsNullOrEmpty(wwwDirPath_) ? "" : Path.Combine(wwwDirPath_, SAVE_DIR_NAME, COMMON_RPGSAVE_NAME);
    public List<string> SaveDataPathes => string.IsNullOrEmpty(wwwDirPath_)
        ? new()
        : new DirectoryInfo(Path.Combine(wwwDirPath_, SAVE_DIR_NAME)).GetFiles(SAVE_RPGSAVE_NAME).Select(x => x.FullName).ToList();

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