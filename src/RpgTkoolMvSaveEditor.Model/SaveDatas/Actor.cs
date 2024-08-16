namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

/// <summary>
/// キャラクター
/// file1.rpgsave::actors::_data::@a の配列要素
/// </summary>
/// <param name="Name">キャラ名 name</param>
/// <param name="HP">HP _hp</param>
/// <param name="MP">MP _mp</param>
/// <param name="TP">TP _tp</param>
/// <param name="Exp">経験値 exp::"1"</param>
public record Actor(string Name, int HP, int MP, int TP, int Exp);
