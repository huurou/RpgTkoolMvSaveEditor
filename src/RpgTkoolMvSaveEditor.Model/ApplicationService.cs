using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.SaveDatas;
using RpgTkoolMvSaveEditor.Model.SaveDatas.Party;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService(Context context, ISaveDataRepository saveDataRepository)
{
    public async Task LoadSaveDataAsync(string wwwDirPath)
    {
        context.WwwDirPath = wwwDirPath;
        if ((await saveDataRepository.LoadAsync()).Unwrap(out var saveData, out _))
        {
            context.SaveData = saveData;
        }
    }

    public void SaveSaveData()
    {
    }
}

public class Context
{
    public string? WwwDirPath { get; set; }
    public SaveData? SaveData { get; set; }
}

public record SaveDataViewDto(
    ImmutableList<SwitchViewDto> Switches,
    ImmutableList<VariableViewDto> Variables,
    int Gold,
    ImmutableList<ActorViewDto> Actors,
    ImmutableList<ItemViewDto> Items,
    ImmutableList<WeaponViewDto> Weapons,
    ImmutableList<ArmorViewDto> Armors
);
public record SwitchViewDto(string Name, string Description, bool? Value);
public record VariableViewDto(string Name, string Description, object? Value);
public record ActorViewDto(string Name, int HP, int MP, int TP, int Level, int Exp);
public record ItemViewDto(string Name, string Description, int HeldCount);
public record WeaponViewDto(string Name, string Description, int HeldCount);
public record ArmorViewDto(string Name, string Description, int HeldCount)
{
    public static ArmorViewDto FromModel(HeldArmor model)
    {
        return new(model.Armor.Name, model.Armor.Description, model.Count);
    }
}
