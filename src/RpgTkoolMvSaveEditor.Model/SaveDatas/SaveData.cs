using RpgTkoolMvSaveEditor.Model.GameData;
using System.Collections.Immutable;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public record SaveData(
    ImmutableList<Switch> Switches,
    ImmutableList<Variable> Variables,
    int Gold,
    ImmutableList<Actor> Actors,
    ImmutableList<Item> Items,
    ImmutableList<Weapon> Weapons,
    ImmutableList<Armor> Armors
);
