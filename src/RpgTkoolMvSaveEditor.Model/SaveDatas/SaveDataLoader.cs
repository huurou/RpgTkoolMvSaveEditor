using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.Events;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public class SaveDataLoader(PathProvider pathProvider, ISaveDataRepository saveDataRepository, ILogger<SaveDataLoader> logger)
{
    public event EventHandler<SaveDataLoadedEventArgs>? SaveDataLoaded;
    public event EventHandler<ErrorOccurredEventArgs>? ErrorOccurred;

    public bool LoadSuppressed { get; set; }

    private FileSystemWatcher? saveDataWather_;
    private CancellationTokenSource? cancellationTokenSource_;

    public async Task LoadAsync()
    {
        StartWatcher();
        await LoadInnerAsync();
    }

    private void StartWatcher()
    {
        if (saveDataWather_?.EnableRaisingEvents == true || pathProvider.WwwDirPath is null) { return; }
        saveDataWather_ = new FileSystemWatcher(Path.Combine(pathProvider.WwwDirPath, "save"), "file.rpgsave");
        saveDataWather_.Changed +=
            async (s, e) =>
            {
                logger.LogDebug("セーブデータに変更あり changeType:{changeType} name:{name} fullPath:{fullPath}", e.ChangeType, e.Name, e.FullPath);
                cancellationTokenSource_?.Cancel();
                cancellationTokenSource_ = new();
                try
                {
                    await Task.Delay(100, cancellationTokenSource_.Token);
                    await LoadInnerAsync();
                }
                catch (OperationCanceledException)
                {
                    logger.LogDebug("セーブデータのロードがキャンセルされました。");
                }
            };
        saveDataWather_.EnableRaisingEvents = true;
    }

    private async Task LoadInnerAsync()
    {
        if (LoadSuppressed)
        {
            logger.LogDebug("セーブデータのロードが抑制されました。");
            LoadSuppressed = false;
            return;
        }
        if ((await saveDataRepository.LoadAsync()).Unwrap(out var commonSaveData, out var message))
        {
            SaveDataLoaded?.Invoke(this, new(commonSaveData));
        }
        else
        {
            ErrorOccurred?.Invoke(this, new(message));
        }
    }
}
