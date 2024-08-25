using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameData.Systems;

public record SystemDataDto(ImmutableList<string> Switches, ImmutableList<string> Variables);
