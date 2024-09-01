using Microsoft.Extensions.Logging;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public class SaveDataSaver(SaveDataLoader saveDataLoader, ISaveDataRepository saveDataRepository, ILogger<SaveDataSaver> logger)
{
    private CancellationTokenSource? cancellationTokenSource_;

    public async Task SaveAsync(SaveData saveData)
    {
        logger.LogInformation("セーブデータのセーブが要求されました。");
        cancellationTokenSource_?.Cancel();
        cancellationTokenSource_ = new();
        try
        {
            await Task.Delay(100, cancellationTokenSource_.Token);
            await saveDataRepository.SaveAsync(saveData);
            saveDataLoader.LoadSuppressed = true;
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("セーブデータのセーブがキャンセルされました。");
        }
    }
}
