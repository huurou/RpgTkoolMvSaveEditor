namespace RpgTkoolMvSaveEditor.Model.Armors;

/// <summary>
/// Armorのデータ用DTO
/// www/data/Armors.Json の配列要素
/// </summary>
public record ArmorDataDto(int Id, string Name, string Description)
{
    public Armor ToModel()
    {
        return new(new(Id), new(Name), new(Description));
    }
}
