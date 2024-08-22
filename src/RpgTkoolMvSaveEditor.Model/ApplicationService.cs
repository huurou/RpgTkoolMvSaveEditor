using RpgTkoolMvSaveEditor.Model.SaveDatas;

namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService(Context context, ISaveDataRepository saveDataRepository)
{
    public async Task LoadSaveDataAsync(string wwwDirPath)
    {
        context.WwwDirPath = wwwDirPath;
        if ((await saveDataRepository.LoadAsync()).Unwrap(out var saveData, out _))
        {
            context.SaveData = saveData;
        }
    }

    public async Task SaveSaveDataAsync(SaveDataViewDto dto)
    {
        await saveDataRepository.SaveAsync(dto.ToModel());
    }
}

public class Context
{
    public string? WwwDirPath { get; set; }
    public SaveData? SaveData { get; set; }
}
