namespace RpgTkoolMvSaveEditor.Application;

public class ApplicationService
{
    public event EventHandler<string>? ErrorOcuured;
    /// <summary>
    /// ゲームフォルダかwwwフォルダを読み込む
    /// </summary>
    /// <param name="dirPath">ゲームフォルダかwwwフォルダ</param>
    /// <returns>成功/失敗<para/>
    /// wwwフォルダでない、あるいは直下にwwwフォルダがない場合失敗
    /// wwwフォルダ直下に必要なフォルダ、ファイルがない場合失敗</returns>
    public bool LoadDirectory(string dirPath)
    {
        var wwwDir = Path.GetDirectoryName(dirPath) == "www" ? dirPath
            : Directory.GetDirectories(dirPath).Contains("www") ? Path.Combine(dirPath, "www")
            : null;
        if (wwwDir is null)
        {
            ErrorOcuured?.Invoke(this, "ゲームフォルダかwwwフォルダを指定してください。");
            return false;
        }

        return true;
    }
}