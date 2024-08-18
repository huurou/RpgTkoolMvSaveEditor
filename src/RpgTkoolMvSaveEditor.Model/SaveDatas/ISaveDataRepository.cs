using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public interface ISaveDataRepository
{
    Task<Result<SaveData>> LoadAsync();

    Task SaveAsync(SaveData saveData);
}
