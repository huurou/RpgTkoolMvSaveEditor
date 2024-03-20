using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class Variables : IVariables
{
    public event EventHandler<(int index, object? value)>? ValueChanged;

    public object? this[int index]
    {
        get => list_[index];
        set
        {
            var count = list_.Count;
            if (index >= count)
            {
                for (var i = 0; i < index - count + 1; i++)
                {
                    list_.Add(null);
                }
            }
            if (list_[index] == value)
            {
                return;
            }

            list_[index] = value;
            ValueChanged?.Invoke(this, (index, value));
        }
    }

    private readonly List<object?> list_ = [];

    public int Count => list_.Count;

    public Variables(JsonNode? node)
    {
        if (node is null)
        {
            return;
        }

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