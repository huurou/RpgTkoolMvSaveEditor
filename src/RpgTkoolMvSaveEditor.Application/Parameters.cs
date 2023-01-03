using RpgTkoolMvSaveEditor.Domain.SaveDatas;

namespace RpgTkoolMvSaveEditor.Application;

public class Parameters
{
    public int Gold { get; }

    public Parameters(IParameters parameters)
    {
        Gold = parameters.Gold;
    }
}