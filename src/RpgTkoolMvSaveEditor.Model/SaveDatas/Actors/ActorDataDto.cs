namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Actors;

/// <summary>
/// アクターのデータ用DTO
/// file1.rpgsave::actors::_data::@a の配列要素
/// </summary>
/// <param name="Name">キャラ名 name</param>
/// <param name="HP">HP _hp</param>
/// <param name="MP">MP _mp</param>
/// <param name="TP">TP _tp</param>
/// <param name="Level">Level _level</param>
/// <param name="Exp">経験値 exp::"1"</param>
public record ActorDataDto(string Name, int HP, int MP, int TP, int Level, int Exp)
{
    public static ActorDataDto FromModel(Actor model)
    {
        return new(model.Name, model.HP, model.MP, model.TP, model.Level, model.Exp);
    }

    public Actor ToModel()
    {
        return new(Name, HP, MP, TP, Level, Exp);
    }
}
