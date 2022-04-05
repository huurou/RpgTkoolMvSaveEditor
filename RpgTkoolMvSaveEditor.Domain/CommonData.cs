using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Domain;

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

public interface IGameSwitches : IEnumerable<KeyValuePair<int, bool>>
{
    event EventHandler<JsonNode>? NodeChanged;

    List<int> Keys { get; }
    List<bool> Values { get; }
    int Count { get; }
    bool this[int key] { get; set; }

    bool ContainsKey(int key);

    bool TryGetValue(int key, out bool value);

    bool Contains(KeyValuePair<int, bool> item);
}

public interface IGameVariables: IEnumerable<KeyValuePair<int, int>>
{
    event EventHandler<JsonNode>? NodeChanged;

    List<int> Keys { get; }
    List<int> Values { get; }
    int Count { get; }
    int this[int key] { get; set; }

    bool ContainsKey(int key);

    bool TryGetValue(int key, out int value);

    bool Contains(KeyValuePair<int, int> item);
}
