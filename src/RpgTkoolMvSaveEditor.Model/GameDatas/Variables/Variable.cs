namespace RpgTkoolMvSaveEditor.Model.Variables;

/// <summary>
/// 変数
/// </summary>
/// <param name="Index">System.json::variables配列でのインデックス</param>
/// <param name="Name">変数名</param>
/// <param name="Value">変数の値</param>
public record Variable(VariableIndex Index, VariableName Name, VariableValue Value);
