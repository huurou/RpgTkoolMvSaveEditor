using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.Items;

public interface IItemsLoader
{
    Task<Result<List<Item>>> LoadAsync();
}
