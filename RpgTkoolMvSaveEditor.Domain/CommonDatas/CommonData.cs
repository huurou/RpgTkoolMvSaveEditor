namespace RpgTkoolMvSaveEditor.Domain.CommonDatas;

public class CommonData
{
    public IGameSwitches GameSwitches { get; }
    public IGameVariables GameVariables { get; }

    public CommonData(IGameSwitches gameSwitches, IGameVariables gameVariables)
    {
        GameSwitches = gameSwitches;
        GameVariables = gameVariables;
    }
}