using RpgTkoolMvSaveEditor.Model.Items;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

/// <summary>
/// 所持アイテム
/// www/save/file1.rpgsave::party::_items の配列要素
/// </summary>
/// <param name="Item">アイテム</param>
/// <param name="Count">所持数</param>
public record HeldItem(Item Item, int Count);
