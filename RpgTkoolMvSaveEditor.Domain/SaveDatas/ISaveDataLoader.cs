namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface ISaveDataLoader
{
    SaveData? Load(string path);
}