using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

public class Parameters : IParameters
{
    public event EventHandler<int>? GoldChanged;

    public int Gold
    {
        get => gold_;
        set
        {
            gold_ = value;
            GoldChanged?.Invoke(this, value);
        }
    }
    private int gold_;

    public Parameters(JsonNode? goldNode)
    {
        if (goldNode is not null)
        {
            gold_ = goldNode.GetValue<int>();
        }
    }
}