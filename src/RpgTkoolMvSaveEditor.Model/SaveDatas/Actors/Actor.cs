namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Actors;

/// <summary>
/// アクター
/// </summary>
/// <param name="Name">名前</param>
/// <param name="HP">HP</param>
/// <param name="MP">MP</param>
/// <param name="TP">TP</param>
/// <param name="Level">レベル</param>
/// <param name="Exp">経験値</param>
public record Actor(string Name, int HP, int MP, int TP, int Level, int Exp);
