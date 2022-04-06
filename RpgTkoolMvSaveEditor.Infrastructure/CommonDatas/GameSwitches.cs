using RpgTkoolMvSaveEditor.Domain.CommonDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.CommonDatas;

public class GameSwitches : IGameSwitches
{
    public event EventHandler<KeyValuePair<string, bool?>>? PropertyChanged;

    private readonly Dictionary<string, bool?> dict_ = new();

    public bool? this[string key]
    {
        get => dict_[key];
        set
        {
            if (dict_[key] == value) return;
            dict_[key] = value;
            PropertyChanged?.Invoke(this, new(key, value));
        }
    }

    public GameSwitches(JsonNode node)
    {
        foreach (var prop in node.AsObject().AsEnumerable())
        {
            // "@1"のような@から始まる組を省く
            if (int.TryParse(prop.Key, out _)) dict_[prop.Key] = prop.Value?.GetValue<bool?>();
        }
    }

    public IEnumerator<KeyValuePair<string, bool?>> GetEnumerator()
    {
        return dict_.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}