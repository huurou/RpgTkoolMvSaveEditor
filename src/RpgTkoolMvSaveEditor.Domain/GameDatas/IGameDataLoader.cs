namespace RpgTkoolMvSaveEditor.Domain;

public interface IGameDataLoader
{
    Task<TGameData?> LoadAsync<TGameData>(string path);
}