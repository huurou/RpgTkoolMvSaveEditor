using RpgTkoolMvSaveEditor.Model.GameData;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public record CommonSaveData(ImmutableList<Switch> GameSwitches, ImmutableList<Variable> GameVariables);
