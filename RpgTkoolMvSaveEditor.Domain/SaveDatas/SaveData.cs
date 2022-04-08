namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public class SaveData
{
    public ISwitches Switches { get; }
    public IVariables Variables { get; }
    public IItems Items { get; }
    public IArmors Armors { get; }

    public SaveData(ISwitches switches, IVariables variables, IItems items, IArmors armors)
    {
        Switches = switches;
        Variables = variables;
        Items = items;
        Armors = armors;
    }
}