using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameData.Systems;

/// <summary>
/// システムデータのデータ用DTO
/// </summary>
/// <param name="Switchs">意味的にはSwichNamesだがSystem.jsonでは"switches"</param>
/// <param name="Variables">意味的にはVariableNamesだがSYstem.jsonでは"variables"</param>
public record SystemDataDto(ImmutableList<string> Switches, ImmutableList<string> Variables);
