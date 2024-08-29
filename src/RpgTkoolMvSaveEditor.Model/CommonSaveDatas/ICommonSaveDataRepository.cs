using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public interface ICommonSaveDataRepository
{
    Task<Result<CommonSaveData>> LoadAsync();

    Task<Result> SaveAsync(CommonSaveData commonSaveData);
}
