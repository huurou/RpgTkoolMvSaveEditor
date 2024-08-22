using RpgTkoolMvSaveEditor.Model.Variables;

namespace RpgTkoolMvSaveEditor.Model;

public record VariableViewDto(string Name, object? Value)
{
    public static VariableViewDto FromModel(Variable model)
    {
        return new(model.Name, model.Value);
    }

    public Variable ToModel(int id)
    {
        return new(new(id), Name, Value);
    }
}
