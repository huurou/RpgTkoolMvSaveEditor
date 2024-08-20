namespace RpgTkoolMvSaveEditor.Model.Items;

/// <summary>
/// アイテム
/// </summary>
/// <param name="Id">アイテムID</param>
/// <param name="Name">アイテム名</param>
/// <param name="Description">アイテムの説明</param>
public record Item(ItemId Id, string Name, string Description);
