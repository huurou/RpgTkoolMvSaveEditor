using RpgTkoolMvSaveEditor.Model.Switches;

namespace RpgTkoolMvSaveEditor.Model;

public record SwitchViewDto(string Name, bool? Value)
{
    public static SwitchViewDto FromModel(Switch model)
    {
        return new(model.Name, model.Value);
    }

    public Switch ToModel(int id)
    {
        return new(new(id), Name, Value);
    }
}
