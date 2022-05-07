using RpgTkoolMvSaveEditor.Domain.CommonDatas;

namespace RpgTkoolMvSaveEditor.Domain;

public interface ICommonDataLoader
{
    Task<CommonData?> LoadAsync(string path);
}