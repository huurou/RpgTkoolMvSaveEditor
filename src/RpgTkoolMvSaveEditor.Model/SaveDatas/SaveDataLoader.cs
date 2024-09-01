using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.Events;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public class SaveDataLoader(PathProvider pathProvider, ISaveDataRepository saveDataRepository, ILogger<SaveDataLoader> logger)
{
    public event EventHandler<SaveDataLoadedEventArgs>? SaveDataLoaded;

    public bool LoadSuppressed { get; set; }

    private FileSystemWatcher? saveDataWather_;
    private CancellationTokenSource? cancellationTokenSource_;

    public async Task LoadAsync()
    {
        if (!StartWatcher()) { return; }
        await LoadInnerAsync();
    }

    private bool StartWatcher()
    {
        if (pathProvider.WwwDirPath is null)
        {
            logger.LogError("wwwフォルダが選択されていません。");
            return false;
        }
        if (!Directory.Exists(Path.Combine(pathProvider.WwwDirPath, "save")))
        {
            logger.LogError("{}が存在しません。", Path.Combine(pathProvider.WwwDirPath, "save"));
            return false;
        }
        if (saveDataWather_?.EnableRaisingEvents == true)
        {
            saveDataWather_.EnableRaisingEvents = false;
            saveDataWather_.Dispose();
        }
        saveDataWather_ = new FileSystemWatcher(Path.Combine(pathProvider.WwwDirPath, "save"), "file1.rpgsave");
        saveDataWather_.Changed +=
            async (s, e) =>
            {
                logger.LogInformation("セーブデータに変更あり");
                cancellationTokenSource_?.Cancel();
                cancellationTokenSource_ = new();
                try
                {
                    await Task.Delay(100, cancellationTokenSource_.Token);
                    await LoadInnerAsync();
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("セーブデータのロードがキャンセルされました。");
                }
            };
        saveDataWather_.EnableRaisingEvents = true;
        return true;
    }

    private async Task LoadInnerAsync()
    {
        if (LoadSuppressed)
        {
            logger.LogInformation("セーブデータのロードが抑制されました。");
            LoadSuppressed = false;
            return;
        }
        if ((await saveDataRepository.LoadAsync()).Unwrap(out var saveData, out var message))
        {
            SaveDataLoaded?.Invoke(this, new(saveData));
        }
        else
        {
            logger.LogError("{}", message);
            logger.LogError("セーブデータのロードに失敗しました。");
        }
    }
}
