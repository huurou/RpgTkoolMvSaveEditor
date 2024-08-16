namespace RpgTkoolMvSaveEditor.Model.CommonDatas;

/// <summary>
/// ゲーム共通データ
/// www/save/common.rpgsave
/// </summary>
/// <param name="GameSwitches">ゲーム共通スイッチ</param>
/// <param name="GameVariables">ゲーム共通変数</param>
public record class CommonData(GameSwitches GameSwitches, GameVariables GameVariables);
