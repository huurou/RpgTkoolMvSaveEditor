using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.Armors;

public interface IArmorsLoader
{
    /// <summary>
    /// 防具一覧をロードする
    /// </summary>
    /// <returns>防具一覧</returns>
    Task<Result<List<Armor>>> LoadAsync();
}
