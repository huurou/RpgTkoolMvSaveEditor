using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameDatas.Systems;

/// <summary>
/// システムデータ
/// www/data/System.Json
/// </summary>
/// <param name="SwitchNames">スイッチ名のリスト スイッチIDとインデックスが対応している</param>
/// <param name="VariableNames">変数名のリスト 変数IDとインデックスが対応している</param>
public record System(ImmutableList<SwitchName> SwitchNames, ImmutableList<VariableName> VariableNames);
