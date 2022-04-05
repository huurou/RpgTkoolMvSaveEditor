namespace RpgTkoolMvSaveEditor.Domain.CommonDatas;

public interface IGameVariables : IEnumerable<KeyValuePair<int, int>>
{
    event EventHandler<KeyValuePair<int, int>>? PropertyChanged;

    List<int> Keys { get; }
    List<int> Values { get; }
    int Count { get; }
    int this[int key] { get; set; }

    bool ContainsKey(int key);

    bool TryGetValue(int key, out int value);

    bool TrySetValue(int key, int value);

    bool Contains(KeyValuePair<int, int> item);
}