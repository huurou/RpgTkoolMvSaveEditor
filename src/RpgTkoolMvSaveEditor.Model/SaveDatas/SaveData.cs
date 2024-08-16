using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

/// <summary>
/// セーブデータ
/// www/save/file1.rpgsave
/// </summary>
/// <param name="Gold">ゴールド www/save/file1.rpgsave::party::_gold</param>
/// <param name="Switches">スイッチのリスト www/save/file1.rpgsave::switches_::data::@a</param>
/// <param name="Variables">変数のリスト www/save/file1.rpgsave::variables_::data::@a</param>
/// <param name="HeldItems">所持アイテムのリスト www/save/file1.rpgsave::party::_items</param>
/// <param name="HeldWeapons">所持武器のリスト www/save/file1.rpgsave::party::_weapons</param>
/// <param name="HeldArmors">所持防具のリスト www/save/file1.rpgsave::party::_armors</param>
public record SaveData(
    Gold Gold,
    ImmutableList<Switch> Switches,
    ImmutableList<Variable> Variables,
    ImmutableList<HeldItem> HeldItems,
    ImmutableList<HeldWeapon> HeldWeapons,
    ImmutableList<HeldArmor> HeldArmors,
    ImmutableList<Actor> Actors
);
