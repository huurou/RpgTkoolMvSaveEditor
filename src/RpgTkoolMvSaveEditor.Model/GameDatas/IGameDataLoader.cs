namespace RpgTkoolMvSaveEditor.Model.GameDatas;

public interface IGameDataLoader
{
    Task<T?> LoadAsync<T>(string wwwDirPath);
}
