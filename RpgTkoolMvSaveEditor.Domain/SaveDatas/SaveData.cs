namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public class SaveData
{
    public ISwitches Swiches { get; }

    public IVariables Variables { get; }

    public SaveData(ISwitches swiches, IVariables variables)
    {
        Swiches = swiches;
        Variables = variables;
    }
}