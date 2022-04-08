using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.CommonDatas;

public class GameVariables : IGameVariables
{
    public event EventHandler<KeyValuePair<string, object?>>? PropertyChanged;

    private readonly Dictionary<string, object?> dict_ = new();

    public object? this[string id]
    {
        get => dict_[id];
        set
        {
            if (dict_.ContainsKey(id) && dict_[id] == value) return;
            dict_[id] = value;
            PropertyChanged?.Invoke(this, new(id, value));
        }
    }

    public GameVariables(JsonNode node)
    {
        foreach (var prop in node.AsObject().AsEnumerable())
        {
            // "@1"のような@から始まる組を省く
            if (int.TryParse(prop.Key, out _)) dict_[prop.Key] = prop.Value;
        }
    }

    public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
    {
        return dict_.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}