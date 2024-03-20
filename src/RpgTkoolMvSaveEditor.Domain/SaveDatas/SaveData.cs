namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public class SaveData(IParameters parameters, ISwitches switches, IVariables variables, IItems items, IWeapons weapons, IArmors armors, IActors actors)
{
    public IParameters Parameters { get; } = parameters;
    public ISwitches Switches { get; } = switches;
    public IVariables Variables { get; } = variables;
    public IItems Items { get; } = items;
    public IWeapons Weapons { get; } = weapons;
    public IArmors Armors { get; } = armors;
    public IActors Actors { get; } = actors;
}