namespace RpgTkoolMvSaveEditor.Model.Switches;

/// <summary>
/// スイッチ
/// いわゆるフラグ
/// </summary>
/// <param name="Index">SystemDataのswitches配列でのインデックス</param>
/// <param name="Name">スイッチ名</param>
/// <param name="Value">スイッチの値</param>
public record Switch(SwitchIndex Index, SwitchName Name, SwitchValue Value);
