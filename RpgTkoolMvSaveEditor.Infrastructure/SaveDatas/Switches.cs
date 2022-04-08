using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class Switches : ISwitches
{
    public event EventHandler<(int index, bool? value)>? PropertyChanged;

    public bool? this[int index]
    {
        get => list_[index];
        set
        {
            if (list_[index] == value) return;
            list_[index] = value;
            PropertyChanged?.Invoke(this, (index, value));
        }
    }

    private readonly List<bool?> list_ = new();

    public int Count => list_.Count;

    public Switches(JsonNode node)
    {
        var array = node.AsArray();
        list_.Capacity = array.Count;
        foreach (var item in array)
        {
            list_.Add(item?.GetValue<bool?>());
        }
    }

    public IEnumerator<bool?> GetEnumerator()
    {
        return list_.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}