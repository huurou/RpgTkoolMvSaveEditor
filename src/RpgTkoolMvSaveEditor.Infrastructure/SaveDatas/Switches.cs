﻿using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class Switches : ISwitches
{
    public event EventHandler<(int index, bool? value)>? ValueChanged;

    public bool? this[int index]
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
            list_[index] = value;
            ValueChanged?.Invoke(this, (index, value));
        }
    }

    private readonly List<bool?> list_ = [];

    public int Count => list_.Count;

    public Switches(JsonNode? node)
    {
        if (node is null)
        {
            return;
        }

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