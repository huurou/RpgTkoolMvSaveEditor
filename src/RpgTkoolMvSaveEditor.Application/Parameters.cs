using RpgTkoolMvSaveEditor.Domain.SaveDatas;

namespace RpgTkoolMvSaveEditor.Application;

public class Parameters(IParameters parameters)
{
    public int Gold { get; } = parameters.Gold;
}