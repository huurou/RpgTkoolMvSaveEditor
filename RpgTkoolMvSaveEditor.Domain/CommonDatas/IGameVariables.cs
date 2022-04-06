namespace RpgTkoolMvSaveEditor.Domain.CommonDatas;

public interface IGameVariables : IEnumerable<KeyValuePair<string, object?>>
{
    event EventHandler<KeyValuePair<string, object?>>? PropertyChanged;

    object? this[string key] { get; set; }
}