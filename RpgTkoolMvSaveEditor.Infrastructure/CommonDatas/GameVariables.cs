using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.CommonDatas;

public class GameVariables : IGameVariables
{
    public event EventHandler<KeyValuePair<string, object?>>? PropertyChanged;

    private readonly Dictionary<string, object?> dict_ = new();

    public object? this[string key]
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