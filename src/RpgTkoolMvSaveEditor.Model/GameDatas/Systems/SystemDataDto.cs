using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameDatas.Systems;

public record SystemDataDto(ImmutableList<string> Switches, ImmutableList<string> Variables)
{
    public System ToModel()
    {
        return new(
            [.. Switches.Select(x => new SwitchName(x))],
            [.. Variables.Select(x => new VariableName(x))]
        );
    }
}
