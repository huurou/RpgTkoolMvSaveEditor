using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.Events;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataLoader(PathProvider pathProvider, ICommonSaveDataRepository commonSaveDataRepository, ILogger<CommonSaveDataLoader> logger)
{
    public event EventHandler<CommonSaveDataLoadedEventArgs>? CommonSaveDataLoaded;
    public event EventHandler<ErrorOccurredEventArgs>? ErrorOccurred;

    public bool LoadSuppressed { private get; set; }

    private FileSystemWatcher? commonSaveDataWather_;
    private CancellationTokenSource? cancellationTokenSource_;

    public async Task LoadAsync()
    {
        StartWatcher();
        await LoadInnerAsync();
    }

    private void StartWatcher()
    {
        if (commonSaveDataWather_?.EnableRaisingEvents == true || pathProvider.WwwDirPath is null) { return; }
        commonSaveDataWather_ = new FileSystemWatcher(Path.Combine(pathProvider.WwwDirPath, "save"), "common.rpgsave");
        commonSaveDataWather_.Changed +=
            async (s, e) =>
            {
                logger.LogDebug("共通セーブデータに変更あり changeType:{changeType} name:{name} fullPath:{fullPath}", e.ChangeType, e.Name, e.FullPath);
                cancellationTokenSource_?.Cancel();
                cancellationTokenSource_ = new();
                try
                {
                    await Task.Delay(100, cancellationTokenSource_.Token);
                    await LoadInnerAsync();
                }
                catch (OperationCanceledException)
                {
                    logger.LogDebug("共通セーブデータのロードがキャンセルされました。");
                }
            };
        commonSaveDataWather_.EnableRaisingEvents = true;
    }

    private async Task LoadInnerAsync()
    {
        if (LoadSuppressed)
        {
            logger.LogDebug("共通セーブデータのロードが抑制されました。");
            LoadSuppressed = false;
            return;
        }
        if ((await commonSaveDataRepository.LoadAsync()).Unwrap(out var commonSaveData, out var message))
        {
            CommonSaveDataLoaded?.Invoke(this, new(commonSaveData));
        }
        else
        {
            ErrorOccurred?.Invoke(this, new(message));
        }
    }
}
