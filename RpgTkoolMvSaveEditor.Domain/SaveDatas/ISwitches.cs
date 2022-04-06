namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface ISwitches : IEnumerable<bool?>
{
    event EventHandler<(int index, bool? value)>? PropertyChanged;

    bool? this[int index] { get; set; }
}