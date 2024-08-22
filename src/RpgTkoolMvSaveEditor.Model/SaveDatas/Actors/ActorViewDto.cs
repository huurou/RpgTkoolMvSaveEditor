using RpgTkoolMvSaveEditor.Model.SaveDatas.Actors;

namespace RpgTkoolMvSaveEditor.Model;

public record ActorViewDto(string Name, int HP, int MP, int TP, int Level, int Exp)
{
    public static ActorViewDto FromModel(Actor model)
    {
        return new(model.Name, model.HP, model.MP, model.TP, model.Level, model.Exp);
    }

    public Actor ToModel()
    {
        return new(Name, HP, MP, TP, Level, Exp);
    }
}
