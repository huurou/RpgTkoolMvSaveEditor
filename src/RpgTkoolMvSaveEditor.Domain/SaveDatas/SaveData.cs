namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public class SaveData
{
    public IParameters Parameters { get; }
    public ISwitches Switches { get; }
    public IVariables Variables { get; }
    public IItems Items { get; }
    public IWeapons Weapons { get; }
    public IArmors Armors { get; }
    public IActors Actors { get; }

    public SaveData(IParameters parameters, ISwitches switches, IVariables variables, IItems items, IWeapons weapons, IArmors armors, IActors actors)
    {
        Parameters = parameters;
        Switches = switches;
        Variables = variables;
        Items = items;
        Weapons = weapons;
        Armors = armors;
        Actors = actors;
    }
}