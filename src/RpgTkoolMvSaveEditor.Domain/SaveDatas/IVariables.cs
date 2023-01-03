namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface IVariables : IEnumerable<object?>
{
    event EventHandler<(int index, object? value)>? ValueChanged;

    object? this[int index] { get; set; }

    int Count { get; }
}
