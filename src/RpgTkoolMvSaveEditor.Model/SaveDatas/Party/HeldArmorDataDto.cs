namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

/// <summary>
/// 所持防具のデータ用DTO
/// www/save/file1.rpgsave::party::_armors の配列要素
/// </summary>
/// <param name="Id">防具ID</param>
/// <param name="Count">所持数</param>
public record HeldArmorDataDto(int Id, int Count);
