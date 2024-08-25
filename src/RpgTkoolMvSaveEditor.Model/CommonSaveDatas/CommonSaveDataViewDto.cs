using RpgTkoolMvSaveEditor.Model.GameData.Switches;
using RpgTkoolMvSaveEditor.Model.GameData.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.CommonSaveDatas;

public record CommonSaveDataViewDto(
    ImmutableList<SwitchViewDto> GameSwitches,
    ImmutableList<VariableViewDto> GameVariables
);
