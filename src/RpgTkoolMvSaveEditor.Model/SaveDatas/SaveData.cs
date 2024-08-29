using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;

public record SaveData(
    ImmutableList<Switch> Switches,
    ImmutableList<Variable> Variables,
    int Gold,
    ImmutableList<Actor> Actors,
    ImmutableList<Item> Items,
    ImmutableList<Weapon> Weapons,
    ImmutableList<Armor> Armors
);
