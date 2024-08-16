using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameDatas;

/// <summary>
/// システムデータ
/// www/data/System.Json
/// </summary>
/// <param name="GameTitle">ゲームタイトル</param>
/// <param name="SwitchNames">スイッチ名のリスト</param>
/// <param name="VariableNames">変数名のリスト</param>
public record SystemData(string GameTitle, ImmutableList<SwitchName> SwitchNames, ImmutableList<VariableName> VariableNames);
