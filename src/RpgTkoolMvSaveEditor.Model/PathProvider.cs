namespace RpgTkoolMvSaveEditor.Model;

public class PathProvider(string[]? args = default)
{
    // 引数でファイルパスを渡すとインターフェースの汎用性が下がるのでコンストラクタで渡すようにした
    public string? WwwDirPath { get; } = args?.Length > 0 ? args[0] : null;
}