using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.CommonDatas;

public class GameVariables : IGameVariables
{
    public event EventHandler<KeyValuePair<int, int>>? PropertyChanged;

    private readonly Dictionary<int, int> dict_ = new();

    public List<int> Keys => dict_.Keys.ToList();

    public List<int> Values => dict_.Values.ToList();

    public int Count => dict_.Count;

    public int this[int key]
    {
        get => dict_[key];
        set
        {
            if (dict_[key] == value) return;
            dict_[key] = value;
            PropertyChanged?.Invoke(this, new(key, value));
        }
    }

    public GameVariables(JsonNode node)
    {
        foreach (var prop in node.AsObject().AsEnumerable())
        {
            // "@1"のような@から始まる組を省く
            if (!int.TryParse(prop.Key, out var num)) continue;
            dict_[num] = (int)prop.Value!;
        }
    }

    public bool ContainsKey(int key)
    {
        return dict_.ContainsKey(key);
    }

    public bool TryGetValue(int key, out int value)
    {
        return dict_.TryGetValue(key, out value);
    }

    public bool TrySetValue(int key, int value)
    {
        if (ContainsKey(key))
        {
            this[key] = value;
            return true;
        }
        else return false;
    }

    public bool Contains(KeyValuePair<int, int> item)
    {
        return dict_.Contains(item);
    }

    public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
    {
        return dict_.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}