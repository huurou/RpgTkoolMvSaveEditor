using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.GameDatas.Systems;

public interface ISystemLoader
{
    Task<Result<System>> LoadAsync();
}
