using RpgTkoolMvSaveEditor.Domain;
using System.Collections;
using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Infrastructure;

public class CommonDataLoader : ICommonDataLoader
{
    private const string GAME_SWITCHES = "gameSwitches";
    private const string GAME_VARIABLES = "gameVariables";

    private readonly ISaveDataConverter converter_;

    public CommonDataLoader(ISaveDataConverter saveDataConverter)
    {
        converter_ = saveDataConverter;
    }

    public CommonData Load(string path)
    {
        var rootNode = converter_.ToJsonNode(path);
        var commonData = new CommonData(new GameSwitches(rootNode[GAME_SWITCHES]!), new GameVariables(rootNode[GAME_VARIABLES]!));

        commonData.GameSwitches.NodeChanged += (s, node) =>
        {
            rootNode[GAME_SWITCHES] = node;
            converter_.FronJsonNode(path, rootNode);
        };
        commonData.GameVariables.NodeChanged += (s, node) =>
        {
            rootNode[GAME_VARIABLES] = node;
            converter_.FronJsonNode(path, rootNode);
        };

        return commonData;
    }

    public class GameSwitches : IGameSwitches
    {
        public event EventHandler<JsonNode>? NodeChanged;

        private readonly JsonNode node_;
        private readonly Dictionary<int, bool> dict_ = new();

        public List<int> Keys => dict_.Keys.ToList();

        public List<bool> Values => dict_.Values.ToList();

        public int Count => dict_.Count;

        public bool this[int key]
        {
            get => dict_[key];
            set
            {
                if (dict_[key] == value) return;
                dict_[key] = value;
                node_[key.ToString()] = value;
                NodeChanged?.Invoke(this, node_);
            }
        }

        public GameSwitches(JsonNode node)
        {
            node_ = node;
            foreach (var prop in node.AsObject().AsEnumerable())
            {
                // "@1"のような@から始まる組を省く
                if (!int.TryParse(prop.Key, out var num)) continue;
                dict_[num] = (bool)prop.Value!;
            }
        }

        public bool ContainsKey(int key)
        {
            return dict_.ContainsKey(key);
        }

        public bool TryGetValue(int key, out bool value)
        {
            return dict_.TryGetValue(key, out value);
        }

        public bool Contains(KeyValuePair<int, bool> item)
        {
            return dict_.Contains(item);
        }

        public IEnumerator<KeyValuePair<int, bool>> GetEnumerator()
        {
            return dict_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class GameVariables : IGameVariables
    {
        public event EventHandler<JsonNode>? NodeChanged;

        private readonly JsonNode node_;
        private readonly Dictionary<int, int> dict_ = new();

        public List<int> Keys => dict_.Keys.ToList();

        public List<int> Values => dict_.Values.ToList();

        public int Count => dict_.Count;

        public int this[int key]
        {
            get => dict_[key];
            set
            {
                if (dict_[key] == value) return;
                dict_[key] = value;
                node_[key.ToString()] = value;
                NodeChanged?.Invoke(this, node_);
            }
        }

        public GameVariables(JsonNode node)
        {
            node_ = node;
            foreach (var prop in node.AsObject().AsEnumerable())
            {
                // "@1"のような@から始まる組を省く
                if (!int.TryParse(prop.Key, out var num)) continue;
                dict_[num] = (int)prop.Value!;
            }
        }

        public bool ContainsKey(int key)
        {
            return dict_.ContainsKey(key);
        }

        public bool TryGetValue(int key, out int value)
        {
            return dict_.TryGetValue(key, out value);
        }

        public bool Contains(KeyValuePair<int, int> item)
        {
            return dict_.Contains(item);
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return dict_.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}