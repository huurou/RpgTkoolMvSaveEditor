using RpgTkoolMvSaveEditor.Domain.GameDatas;
using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class Actors : IActors
{
    private readonly List<ActorData> actors_ = [];

    public Actors(JsonNode? node)
    {
        var a = (node?["_data"])?["@a"];
        if (a is null)
        {
            return;
        }

        var array = a.AsArray();
        foreach (var item in array)
        {
            if (item is null)
            {
                continue;
            }

            actors_.Add(new ActorData
            {
                Name = item["_name"]?.GetValue<string>() ?? "",
                HP = item["_hp"]?.GetValue<int>() ?? default,
                MP = item["_mp"]?.GetValue<int>() ?? default,
                TP = item["_tp"]?.GetValue<int>() ?? default,
                Exp = item["_exp"]?.AsObject().First().Value?.GetValue<int>() ?? default,
            });
        }
    }

    public IEnumerator<ActorData> GetEnumerator()
    {
        return actors_.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class Armors : IArmors
{
    public event EventHandler<(string id, int value)>? ValueChanged;

    private readonly Dictionary<string, int> dict_ = [];

    public int this[string id]
    {
        get => dict_[id];
        set
        {
            if (dict_.TryGetValue(id, out var value1) && value1 == value)
            {
                return;
            }

            dict_[id] = value;
            ValueChanged?.Invoke(this, new(id, value));
        }
    }

    public Armors(JsonNode? node)
    {
        if (node is null)
        {
            return;
        }

        foreach (var prop in node.AsObject().AsEnumerable())
        {
            // "@1"のような@から始まる組を省く
            if (int.TryParse(prop.Key, out _) && prop.Value is not null)
            {
                dict_[prop.Key] = prop.Value.GetValue<int>();
            }
        }
    }

    public bool TryGetValue(string id, out int count)
    {
        return dict_.TryGetValue(id, out count);
    }

    public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
    {
        return dict_.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}