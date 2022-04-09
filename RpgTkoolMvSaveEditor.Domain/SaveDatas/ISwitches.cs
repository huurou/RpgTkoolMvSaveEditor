namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface ISwitches : IEnumerable<bool?>
{
    event EventHandler<(int index, bool? value)>? ValueChanged;

    bool? this[int index] { get; set; }

    int Count { get; }
}