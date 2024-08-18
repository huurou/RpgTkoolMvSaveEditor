using RpgTkoolMvSaveEditor.Model.Armors;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas.Party;

/// <summary>
/// 所持防具
/// </summary>
/// <param name="Armor">防具</param>
/// <param name="Count">所持数</param>
public record HeldArmor(Armor Armor, int Count);
