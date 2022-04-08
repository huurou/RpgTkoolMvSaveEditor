using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class Variables : IVariables
{
    public event EventHandler<(int index, object? value)>? PropertyChanged;

    public object? this[int index]
    {
        get => list_[index];
        set
        {
            if (index >= list_.Count) return;
            if (list_[index] == value) return;
            list_[index] = value;
            PropertyChanged?.Invoke(this, (index, value));
        }
    }

    private readonly List<object?> list_ = new();

    public int Count => list_.Count;

    public Variables(JsonNode node)
    {
        var array = node.AsArray();
        list_.Capacity = array.Count;
        foreach (var item in array)
        {
            list_.Add(item?.GetValue<object?>());
        }
    }

    public IEnumerator<object?> GetEnumerator()
    {
        return list_.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}