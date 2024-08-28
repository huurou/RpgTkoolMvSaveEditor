using RpgTkoolMvSaveEditor.Model.GameData.Actors;
using RpgTkoolMvSaveEditor.Model.GameData.Armors;
using RpgTkoolMvSaveEditor.Model.GameData.Items;
using RpgTkoolMvSaveEditor.Model.GameData.Switches;
using RpgTkoolMvSaveEditor.Model.GameData.Variables;
using RpgTkoolMvSaveEditor.Model.GameData.Weapons;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;

public record SaveDataViewDto(
    ImmutableList<SwitchViewDto> Switches,
    ImmutableList<VariableViewDto> Variables,
    int Gold,
    ImmutableList<ActorViewDto?> Actors,
    ImmutableList<ItemViewDto> Items,
    ImmutableList<WeaponViewDto> Weapons,
    ImmutableList<ArmorViewDto> Armors
);
