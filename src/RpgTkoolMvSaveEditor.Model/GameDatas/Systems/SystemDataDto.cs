using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameDatas.Systems;

public record SystemDataDto(ImmutableList<string> Switches, ImmutableList<string> Variables)
{
    public System ToModel()
    {
        return new(Switches, Variables);
    }
}
