using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.Events;
using RpgTkoolMvSaveEditor.Model.SaveDatas;

namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService
{
    public event EventHandler<CommonSaveDataLoadedEventArgs>? CommonSaveDataLoaded;
    public event EventHandler<SaveDataLoadedEventArgs>? SaveDataLoaded;

    private readonly PathProvider pathProvider_;
    private readonly CommonSaveDataLoader commonSaveDataLoader_;
    private readonly CommonSaveDataSaver commonSaveDataSaver_;
    private readonly SaveDataLoader saveDataLoader_;
    private readonly SaveDataSaver saveDataSaver_;

    public ApplicationService(
        PathProvider pathProvider,
        CommonSaveDataLoader commonSaveDataLoader,
        CommonSaveDataSaver commonSaveDataSaver,
        SaveDataLoader saveDataLoader,
        SaveDataSaver saveDataSaver
    )
    {
        pathProvider_ = pathProvider;
        commonSaveDataLoader_ = commonSaveDataLoader;
        commonSaveDataSaver_ = commonSaveDataSaver;
        saveDataLoader_ = saveDataLoader;
        saveDataSaver_ = saveDataSaver;

        commonSaveDataLoader.CommonSaveDataLoaded += (s, e) => CommonSaveDataLoaded?.Invoke(s, e);
        saveDataLoader.SaveDataLoaded += (s, e) => SaveDataLoaded?.Invoke(s, e);
    }

    public async Task LoadDataAsync()
    {
        if (pathProvider_.WwwDirPath is null) { return; }
        await commonSaveDataLoader_.LoadAsync();
        await saveDataLoader_.LoadAsync();
    }

    public async Task SelectWwwDirAsync(string path)
    {
        pathProvider_.WwwDirPath = path;
        await LoadDataAsync();
    }

    public async Task UpdateCommonSaveDataAsync(CommonSaveData commonSaveData)
    {
        await commonSaveDataSaver_.SaveAsync(commonSaveData);
    }

    public async Task UpdateSaveDataAsync(SaveData saveData)
    {
        await saveDataSaver_.SaveAsync(saveData);
    }
}
