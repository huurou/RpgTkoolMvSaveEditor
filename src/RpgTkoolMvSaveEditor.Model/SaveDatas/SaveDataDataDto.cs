using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.Items;
using RpgTkoolMvSaveEditor.Model.SaveDatas.Actors;
using RpgTkoolMvSaveEditor.Model.SaveDatas.Party;
using RpgTkoolMvSaveEditor.Model.Switches;
using RpgTkoolMvSaveEditor.Model.Variables;
using RpgTkoolMvSaveEditor.Model.Weapons;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

/// <summary>
/// セーブデータのデータ用DTO
/// www/save/file1.rpgsave
/// </summary>
/// <param name="Switches">スイッチのリスト インデックス www/save/file1.rpgsave::switches::_data::@a</param>
/// <param name="Variables">変数のリスト www/save/file1.rpgsave::variables::_data::@a</param>
/// <param name="Gold">ゴールド www/save/file1.rpgsave::party::_gold</param>
/// <param name="Actors">アクターのリスト www/save/file1.rpgsave::actors::_data::@a</param>
/// <param name="HeldItems">所持アイテムのリスト www/save/file1.rpgsave::party::_items</param>
/// <param name="HeldWeapons">所持武器のリスト www/save/file1.rpgsave::party::_weapons</param>
/// <param name="HeldArmors">所持防具のリスト www/save/file1.rpgsave::party::_armors</param>
public record SaveDataDataDto(
    ImmutableList<bool?> Switches,
    ImmutableList<object?> Variables,
    ImmutableList<ActorDataDto?> Actors,
    int Gold,
    ImmutableList<HeldItemDataDto> HeldItems,
    ImmutableList<HeldWeaponDataDto> HeldWeapons,
    ImmutableList<HeldArmorDataDto> HeldArmors
)
{
    public SaveData ToModel(GameDatas.Systems.System system, List<Item> items, List<Weapon> weapons, List<Armor> armors)
    {
        // 全スイッチの数はSystemのスイッチ名配列から分かる 最初の要素は必ずnullなので飛ばす セーブする時にnullを先頭に追加する セーブデータのスイッチ配列に全スイッチの値が入っていないことがあるので長さをチェックする
        var switches = system.SwitchNames.Select((x, i) => (Index: i, Name: x)).Skip(1).Select(x => new Switch(new(x.Index), x.Name, new(x.Index < Switches.Count ? Switches[x.Index] : null)));
        // 全変数の数はSystemの変数名配列から分かる 最初の要素は必ずnullなので飛ばす セーブする時にnullを先頭に追加する セーブデータの変数配列に全変数の値が入っていないことがあるので長さをチェックする
        var variables = system.VariableNames.Select((x, i) => (Index: i, Name: x)).Skip(1).Select(x => new Variable(new(x.Index), x.Name, new(x.Index < Variables.Count ? Variables[x.Index] : null)));
        // 最初の要素は必ずnullなので飛ばす セーブする時にnullを先頭に追加する
        var actors = Actors.Skip(1).Select(x => x?.ToModel());
        var gold = new Gold(Gold);
        var heldItems = HeldItems.Select(x => new HeldItem(items.First(y => x.Id == y.Id.Value), x.Count));
        var heldWeapons = HeldWeapons.Select(x => new HeldWeapon(weapons.First(y => x.Id == y.Id.Value), x.Count));
        var heldArmors = HeldArmors.Select(x => new HeldArmor(armors.First(y => x.Id == y.Id.Value), x.Count));
        return new([.. switches], [.. variables], [.. actors], gold, [.. heldItems], [.. heldWeapons], [.. heldArmors]);
    }
}