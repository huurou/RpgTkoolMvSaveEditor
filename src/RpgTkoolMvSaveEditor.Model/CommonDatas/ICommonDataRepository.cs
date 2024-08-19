using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.CommonDatas;

public interface ICommonDataRepository
{
    Task<Result<CommonData>> LoadAsync();

    Task<Result> SaveAsync(CommonData commonData);
}
