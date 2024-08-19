namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService(Context context)
{
    public void LoadSaveData(string wwwDirPath)
    {
        context.WwwDirPath = wwwDirPath;
    }

    public void SaveSaveData()
    {

    }
}

public class Context
{
    public string? WwwDirPath { get; set; }
}
