namespace RpgTkoolMvSaveEditor.Model.Configs;

public static class Paths
{
    /// <summary>
    /// アプリケーションフォルダパス
    /// </summary>
    public static string ApplicationDir { get; } = CreateAndCombineDir(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppInfo.Name);
    /// <summary>
    /// 設定フォルダパス
    /// </summary>
    public static string SettingsDir { get; } = CreateAndCombineDir(ApplicationDir, "Settings");
    /// <summary>
    /// settings.jsonパス
    /// </summary>
    public static string SettingsJson { get; } = Path.Combine(SettingsDir, "settings.json");
    /// <summary>
    /// ログフォルダパス
    /// </summary>
    public static string LogsDir { get; } = CreateAndCombineDir(ApplicationDir, "Logs");

    private static string CreateAndCombineDir(params string[] paths)
    {
        var path = Path.Combine(paths);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
}