namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

/// <summary>
/// 所持アイテムのデータ用DTO
/// www/save/file1.rpgsave::party::_items の配列要素
/// </summary>
/// <param name="Id">アイテムID</param>
/// <param name="Count">所持数</param>
public record HeldItemDataDto(int Id, int Count);
