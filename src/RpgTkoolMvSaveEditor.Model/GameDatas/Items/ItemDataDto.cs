namespace RpgTkoolMvSaveEditor.Model.Items;

/// <summary>
/// Itemのデータ用DTO
/// www/data/Items.Json の配列要素
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
public record ItemDataDto(int Id, string Name, string Description)
{
    public Item ToModel()
    {
        return new(new(Id), new(Name), new(Description));
    }
}