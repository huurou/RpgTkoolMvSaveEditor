using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataSaver(CommonSaveDataLoader commonSaveDataLoader, ICommonSaveDataRepository commonSaveDataRepository, ILogger<CommonSaveDataSaver> logger)
{
    private CancellationTokenSource cancellationTokenSource_ = new();

    public async Task SaveAsync(CommonSaveData commonSaveData)
    {
        logger.LogInformation("共通セーブデータのセーブが要求されました。");
        cancellationTokenSource_.Cancel();
        cancellationTokenSource_ = new();
        try
        {
            await Task.Delay(100, cancellationTokenSource_.Token);
            await commonSaveDataRepository.SaveAsync(commonSaveData);
            commonSaveDataLoader.LoadSuppressed = true;
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("共通セーブデータのセーブがキャンセルされました。");
        }
    }
}
