namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public class SaveData
{
    public string FileName { get; }
    public ISwitches Switches { get; }
    public IVariables Variables { get; }

    public SaveData(string fileName, ISwitches swiches, IVariables variables)
    {
        FileName = fileName;
        Switches = swiches;
        Variables = variables;
    }
}