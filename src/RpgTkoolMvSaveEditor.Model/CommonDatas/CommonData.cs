using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.CommonDatas;

public record CommonData(ImmutableList<Switch> GameSwitches, ImmutableList<Variable> GameVariables);
