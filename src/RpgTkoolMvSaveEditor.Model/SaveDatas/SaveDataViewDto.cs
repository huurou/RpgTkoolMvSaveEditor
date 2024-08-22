using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.GameDatas.Weapons;
using RpgTkoolMvSaveEditor.Model.Items;
using RpgTkoolMvSaveEditor.Model.Weapons;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public record SaveDataViewDto(
    ImmutableList<SwitchViewDto> Switches,
    ImmutableList<VariableViewDto> Variables,
    int Gold,
    ImmutableList<ActorViewDto> Actors,
    ImmutableList<ItemViewDto> Items,
    ImmutableList<WeaponViewDto> Weapons,
    ImmutableList<ArmorViewDto> Armors
)
{
    public static SaveDataViewDto FromModel(SaveData model, IEnumerable<Item> items, IEnumerable<Weapon> weapons, IEnumerable<Armor> armors)
    {
        var swichDtos = model.Switches.Select(SwitchViewDto.FromModel);
        var variableDtos = model.Variables.Select(VariableViewDto.FromModel);
        var actorDtos = model.Actors.Where(x => x is not null).Select(ActorViewDto.FromModel!);
        var itemDtos = items.Select(x => ItemViewDto.FromModel(model.HeldItems.FirstOrDefault(y => y.Item == x) ?? new(x, 0)));
        var weaponDtos = weapons.Select(x => WeaponViewDto.FromModel(model.HeldWeapons.FirstOrDefault(y => y.Weapon == x) ?? new(x, 0)));
        var armorDtos = armors.Select(x => ArmorViewDto.FromModel(model.HeldArmors.FirstOrDefault(y => y.Armor == x) ?? new(x, 0)));
        return new([.. swichDtos], [.. variableDtos], model.Gold.Value, [.. actorDtos], [.. itemDtos], [.. weaponDtos], [.. armorDtos]);
    }

    public SaveData ToModel()
    {
        var switches = Switches.Select((x, i) => x.ToModel(i + 1));
        var variables = Variables.Select((x, i) => x.ToModel(i + 1));
        var actors = Actors.Select(x => x.ToModel());
        var items = Items.Select((x, i) => x.ToModel(i));
        var weapons = Weapons.Select((x, i) => x.ToModel(i));
        var armors = Armors.Select((x, i) => x.ToModel(i));
        return new([.. switches], [.. variables], new(Gold), [.. actors], [.. items], [.. weapons], [.. armors]);
    }
}
