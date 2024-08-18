using RpgTkoolMvSaveEditor.Model.Items;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

/// <summary>
/// 所持アイテム
/// </summary>
/// <param name="Item">アイテム</param>
/// <param name="Count">所持数</param>
public record HeldItem(Item Item, int Count);
