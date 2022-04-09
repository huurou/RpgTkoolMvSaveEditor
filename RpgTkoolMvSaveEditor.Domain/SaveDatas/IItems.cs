namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface IItems : IEnumerable<KeyValuePair<string, int>>
{
    event EventHandler<(string id, int value)>? ValueChanged;

    int this[string id] { get; set; }

    bool TryGetValue(string id, out int count);
}