using RpgTkoolMvSaveEditor.Model.SaveDatas.Actors;
using RpgTkoolMvSaveEditor.Model.SaveDatas.Party;
using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

/// <summary>
/// セーブデータ
/// </summary>
/// <param name="Switches">スイッチのリスト</param>
/// <param name="Variables">変数のリスト</param>
/// <param name="Gold">ゴールド</param>
/// <param name="Actors">アクターのリスト</param>
/// <param name="HeldItems">所持アイテムのリスト</param>
/// <param name="HeldWeapons">所持武器のリスト</param>
/// <param name="HeldArmors">所持防具のリスト</param>
public record SaveData(
    ImmutableList<Switch?> Switches,
    ImmutableList<Variable?> Variables,
    ImmutableList<Actor?> Actors,
    Gold Gold,
    ImmutableList<HeldItem> HeldItems,
    ImmutableList<HeldWeapon> HeldWeapons,
    ImmutableList<HeldArmor> HeldArmors
);
