using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.CommonDatas;

/// <summary>
/// ゲーム共通スイッチ
/// スイッチインデックス：bool?の集合
/// </summary>
/// <param name="Value">キー：対象スイッチのインデックス 値：対象スイッチの値</param>
public record class GameSwitches(ImmutableDictionary<int, bool?> Value);
