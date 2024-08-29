using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService(Context context)
{
    public void SetWwwDir(string path)
    {
        context.WwwDirPath = path;
    }
}

public class Context(string[]? args = default)
{
    // 引数でファイルパスを渡すとインターフェースの汎用性が下がるのでコンストラクタで渡すようにした
    public string? WwwDirPath { get; set; } = args?.Length > 0 ? args[0] : null;
    public JsonNode? saveDataRootNode_;
    public JsonNode? commonSaveDataRootNode_;
}
