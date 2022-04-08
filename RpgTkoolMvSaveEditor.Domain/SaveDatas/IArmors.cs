namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface IArmors : IEnumerable<KeyValuePair<string, int>>
{
    event EventHandler<(string id, int value)>? PropertyChanged;

    int this[string id] { get; set; }

    bool TryGetValue(string id, out int count);
}
