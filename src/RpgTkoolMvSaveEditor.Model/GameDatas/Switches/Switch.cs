namespace RpgTkoolMvSaveEditor.Model.Switches;

/// <summary>
/// スイッチ
/// いわゆるフラグ
/// </summary>
/// <param name="Id">スイッチID System.jsonのswitches配列でのインデックスと対応している</param>
/// <param name="Name">スイッチ名</param>
/// <param name="Value">スイッチの値</param>
public record Switch(SwitchId Id, string Name, bool? Value);
