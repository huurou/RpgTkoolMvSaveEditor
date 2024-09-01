namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public class SaveDataSaver(SaveDataLoader saveDataLoader, ISaveDataRepository saveDataRepository)
{
    private CancellationTokenSource? cancellationTokenSource_;

    public async Task SaveAsync(SaveData saveData)
    {
        cancellationTokenSource_?.Cancel();
        cancellationTokenSource_ = new();
        try
        {
            await Task.Delay(100, cancellationTokenSource_.Token);
            await saveDataRepository.SaveAsync(saveData);
            saveDataLoader.LoadSuppressed = true;
        }
        catch (OperationCanceledException) { }
    }
}
