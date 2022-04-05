namespace RpgTkoolMvSaveEditor.Domain.CommonDatas;

public interface IGameSwitches : IEnumerable<KeyValuePair<int, bool>>
{
    event EventHandler<KeyValuePair<int, bool>>? PropertyChanged;

    List<int> Keys { get; }
    List<bool> Values { get; }
    int Count { get; }
    bool this[int key] { get; set; }

    bool ContainsKey(int key);

    bool TryGetValue(int key, out bool value);

    public bool TrySetValue(int key, bool value);

    bool Contains(KeyValuePair<int, bool> item);
}