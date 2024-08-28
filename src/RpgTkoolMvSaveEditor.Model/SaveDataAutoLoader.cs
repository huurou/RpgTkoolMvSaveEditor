using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Queries;
using RpgTkoolMvSaveEditor.Model.Queries.Common;
using RpgTkoolMvSaveEditor.Util.Events;
using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model;

public class SaveDataAutoLoader(Context context, IQueryHandler<GetSaveDataQuery, SaveDataViewDto> getSaveDataQueryHandler)
{
    public Event<SaveDataLoadedEventArgs> SaveDataLoaded { get; } = new();
    public Event<ErrorOccurredEventArgs> ErrorOccurred { get; } = new();

    private FileSystemWatcher? saveDataWatcher_;

    public Result Start()
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        saveDataWatcher_ = new(Path.Combine(context.WwwDirPath, "save"), "file1.rpgsave");
        saveDataWatcher_.Changed += async (s, e) =>
        {
            if (context.SaveDataLoadSuppressed)
            {
                context.SaveDataLoadSuppressed = false;
            }
            else if ((await getSaveDataQueryHandler.HandleAsync(new())).Unwrap(out var saveData, out var message))
            {
                SaveDataLoaded.Publish(new(saveData));
            }
            else
            {
                ErrorOccurred.Publish(new(message));
            }
        };
        saveDataWatcher_.EnableRaisingEvents = true;
        return new Ok();
    }

    public void Stop()
    {
        saveDataWatcher_?.Dispose();
    }
}
