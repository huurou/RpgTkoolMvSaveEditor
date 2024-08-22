using RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

namespace RpgTkoolMvSaveEditor.Model;

public record ArmorViewDto(string Name, string Description, int HeldCount)
{
    public static ArmorViewDto FromModel(HeldArmor model)
    {
        return new(model.Armor.Name, model.Armor.Description, model.Count);
    }

    public HeldArmor ToModel(int id)
    {
        return new(new(new(id), Name, Description), HeldCount);
    }
}
