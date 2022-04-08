namespace RpgTkoolMvSaveEditor.Domain.CommonDatas;

public interface IGameSwitches : IEnumerable<KeyValuePair<string, bool?>>
{
    event EventHandler<KeyValuePair<string, bool?>>? PropertyChanged;

    bool? this[string id] { get; set; }
}