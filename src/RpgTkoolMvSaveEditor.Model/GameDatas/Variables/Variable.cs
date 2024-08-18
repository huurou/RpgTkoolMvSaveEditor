namespace RpgTkoolMvSaveEditor.Model.Variables;

/// <summary>
/// 変数
/// </summary>
/// <param name="Id">変数ID System.jsonのvariables配列でのインデックスと対応している</param>
/// <param name="Name">変数名</param>
/// <param name="Value">変数の値</param>
public record Variable(VariableId Id, VariableName Name, VariableValue Value);
