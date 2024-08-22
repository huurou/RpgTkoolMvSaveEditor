using RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

namespace RpgTkoolMvSaveEditor.Model;

public record ItemViewDto(string Name, string Description, int HeldCount)
{
    public static ItemViewDto FromModel(HeldItem model)
    {
        return new(model.Item.Name, model.Item.Description, model.Count);
    }

    public HeldItem ToModel(int id)
    {
        return new(new(new(id), Name, Description), HeldCount);
    }
}
