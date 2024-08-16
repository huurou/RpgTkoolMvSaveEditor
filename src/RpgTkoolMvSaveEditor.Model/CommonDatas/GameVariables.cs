using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.CommonDatas;

/// <summary>
/// ゲーム共通変数
/// 変数インデックス：object?の集合
/// </summary>
/// <param name="Value">キー：対象変数のインデックス 値：対象変数の値</param>
public record class GameVariables(ImmutableDictionary<int, object?> Value);
