using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.Queries;
using RpgTkoolMvSaveEditor.Model.Queries.Common;
using RpgTkoolMvSaveEditor.Util.Events;
using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model;

public class CommonSaveDataAutoLoader(Context context, IQueryHandler<GetCommonSaveDataQuery, CommonSaveDataViewDto> getCommonSaveDataQueryHandler)
{
    public Event<CommonSaveDataLoadedEventArgs> CommonSaveDataLoaded { get; } = new();
    public Event<ErrorOccurredEventArgs> ErrorOccurred { get; } = new();

    private FileSystemWatcher? commonSaveDataWatcher_;

    public Result Start()
    {
        if (context.WwwDirPath is null) { return new Err("wwwフォルダが選択されていません。"); }
        commonSaveDataWatcher_ = new(Path.Combine(context.WwwDirPath, "save"), "common.rpgsave");
        commonSaveDataWatcher_.Changed += async (s, e) =>
        {
            if (context.CommonSaveDataLoadSuppressed)
            {
                context.CommonSaveDataLoadSuppressed = false;
            }
            else if ((await getCommonSaveDataQueryHandler.HandleAsync(new())).Unwrap(out var commonSaveData, out var message))
            {
                CommonSaveDataLoaded.Publish(new(commonSaveData));
            }
            else
            {
                ErrorOccurred.Publish(new(message));
            }
        };
        commonSaveDataWatcher_.EnableRaisingEvents = true;
        return new Ok();
    }

    public void Stop()
    {
        commonSaveDataWatcher_?.Dispose();
    }
}