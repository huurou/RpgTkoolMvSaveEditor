namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface IParameters
{
    event EventHandler<int>? GoldChanged;

    int Gold { get; set; }
}