namespace RpgTkoolMvSaveEditor.Model.Items;

/// <summary>
/// アイテム
/// www/data/Items.Json の配列要素
/// </summary>
/// <param name="Id">アイテムID</param>
/// <param name="Name">アイテム名</param>
/// <param name="Description">アイテムの説明</param>
public record Item(ItemId Id, ItemName Name, ItemDescription Description);
