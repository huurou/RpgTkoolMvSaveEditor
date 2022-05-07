namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface ISaveDataLoader
{
    Task<SaveData?> LoadAsync(string path);
}