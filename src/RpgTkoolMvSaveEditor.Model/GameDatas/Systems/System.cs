using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameDatas.Systems;

/// <summary>
/// システムデータ
/// www/data/System.Json
/// </summary>
/// <param name="SwitchNames">スイッチ名のリスト</param>
/// <param name="VariableNames">変数名のリスト</param>
public record System(ImmutableList<SwitchName> SwitchNames, ImmutableList<VariableName> VariableNames);
