using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.GameDatas.Systems;
using RpgTkoolMvSaveEditor.Model.Items;
using RpgTkoolMvSaveEditor.Model.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Weapons;

namespace RpgTkoolMvSaveEditor.Test;

public class LoadTest
{
    private readonly Context wwwContext_ = new();

    public LoadTest()
    {
        wwwContext_.WwwDirPath = Path.Combine("loadTestData", "www");
    }

    [Fact]
    public async Task ItemsJsonロード()
    {
        // Arrange
        var itemsLoader = new ItemsLoader(wwwContext_);

        // Action
        var result = await itemsLoader.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), "アイテム1", "アイテム1の説明"),
                new(new(2), "アイテム2", "アイテム2の説明"),
            ],
            actual
        );
    }

    [Fact]
    public async Task WeaponsJsonロード()
    {
        // Arrange
        var weaponsLoader = new WeaponsLoader(wwwContext_);

        // Action
        var result = await weaponsLoader.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), "武器1", "武器1の説明"),
                new(new(2), "武器2", "武器2の説明"),
            ],
            actual
        );
    }

    [Fact]
    public async Task ArmorsJsonロード()
    {
        // Arrange
        var armorsLoader = new ArmorsLoader(wwwContext_);

        // Action
        var result = await armorsLoader.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), "防具1", "防具1の説明"),
                new(new(2), "防具2", "防具2の説明"),
            ],
            actual
        );
    }

    [Fact]
    public async Task SystemJsonロード()
    {
        // Arrange
        var systemLoader = new SystemLoader(wwwContext_);

        // Action
        var result = await systemLoader.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                "",
                "スイッチ1",
                "スイッチ2",
                "スイッチ3"
            ],
            actual.SwitchNames
        );
        Assert.Equal(
            [
                "",
                "変数1",
                "変数2",
                "変数3"
            ],
            actual.VariableNames
        );
    }

    [Fact]
    public async Task SaveDataロード()
    {
        // Arrange
        var saveDataRepository = new SaveDataRepository(wwwContext_, new SystemLoader(wwwContext_), new ItemsLoader(wwwContext_), new WeaponsLoader(wwwContext_), new ArmorsLoader(wwwContext_));

        // Action
        var result = await saveDataRepository.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), "スイッチ1", true),
                new(new(2), "スイッチ2", false),
                new(new(3), "スイッチ3", null),
            ],
            actual.Switches
        );
        Assert.Equal(
            [
                new(new(1), "変数1", 1),
                new(new(2), "変数2", "a"),
                new(new(3), "変数3", null),
            ],
            actual.Variables
        );
        Assert.Equal(
            [
                new("Alice", 100, 200, 300, 10, 1000),
                null,
                new("Bob", 400, 500, 600, 20, 4000),
            ],
            actual.Actors
        );
        Assert.Equal(new(123456), actual.Gold);
        Assert.Equal(
            [
                new(new(new(1), "アイテム1", "アイテム1の説明"), 1),
                new(new(new(2), "アイテム2", "アイテム2の説明"), 2),
            ],
            actual.HeldItems
        );
        Assert.Equal(
            [
                new(new(new(1), "武器1", "武器1の説明"), 3),
                new(new(new(2), "武器2", "武器2の説明"), 4),
            ],
            actual.HeldWeapons
        );
        Assert.Equal(
            [
                new(new(new(1), "防具1", "防具1の説明"), 5),
                new(new(new(2), "防具2", "防具2の説明"), 6),
            ],
            actual.HeldArmors
        );
    }
}

// Arrange

// Action

// Assert
