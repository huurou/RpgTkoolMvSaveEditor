using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Model.Events;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public class CommonSaveDataLoader(PathProvider pathProvider, ICommonSaveDataRepository commonSaveDataRepository, ILogger<CommonSaveDataLoader> logger)
{
    public event EventHandler<CommonSaveDataLoadedEventArgs>? CommonSaveDataLoaded;

    public bool LoadSuppressed { private get; set; }

    private FileSystemWatcher? commonSaveDataWather_;
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
        if (commonSaveDataWather_?.EnableRaisingEvents == true)
        {
            commonSaveDataWather_.EnableRaisingEvents = false;
            commonSaveDataWather_.Dispose();
        }
        commonSaveDataWather_ = new FileSystemWatcher(Path.Combine(pathProvider.WwwDirPath, "save"), "common.rpgsave");
        commonSaveDataWather_.Changed +=
            async (s, e) =>
            {
                logger.LogInformation("共通セーブデータに変更あり");
                cancellationTokenSource_?.Cancel();
                cancellationTokenSource_ = new();
                try
                {
                    await Task.Delay(100, cancellationTokenSource_.Token);
                    await LoadInnerAsync();
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("共通セーブデータのロードがキャンセルされました。");
                }
            };
        commonSaveDataWather_.EnableRaisingEvents = true;
        return true;
    }

    private async Task LoadInnerAsync()
    {
        if (LoadSuppressed)
        {
            logger.LogInformation("共通セーブデータのロードが抑制されました。");
            LoadSuppressed = false;
            return;
        }
        if ((await commonSaveDataRepository.LoadAsync()).Unwrap(out var commonSaveData, out var message))
        {
            CommonSaveDataLoaded?.Invoke(this, new(commonSaveData));
        }
        else
        {
            logger.LogError("{}", message);
            logger.LogError("共通セーブデータのロードに失敗しました。");
        }
    }
}
