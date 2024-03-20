namespace RpgTkoolMvSaveEditor.Domain.CommonDatas;

public class CommonData(IGameSwitches gameSwitches, IGameVariables gameVariables)
{
    public IGameSwitches GameSwitches { get; } = gameSwitches;
    public IGameVariables GameVariables { get; } = gameVariables;
}