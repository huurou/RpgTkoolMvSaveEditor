using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.GameDatas.Systems;
using RpgTkoolMvSaveEditor.Model.Items;
using RpgTkoolMvSaveEditor.Model.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Weapons;

namespace RpgTkoolMvSaveEditor.Test;

public class LoadTest
{
    private readonly WwwContext wwwContext_ = new();

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
                new(new(1), new("アイテム1"), new("アイテム1の説明")),
                new(new(2), new("アイテム2"), new("アイテム2の説明")),
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
                new(new(1), new("武器1"), new("武器1の説明")),
                new(new(2), new("武器2"), new("武器2の説明")),
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
                new(new(1), new("防具1"), new("防具1の説明")),
                new(new(2), new("防具2"), new("防具2の説明")),
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
                new(""),
                new("スイッチ1"),
                new("スイッチ2"),
                new("スイッチ3")
            ],
            actual.SwitchNames
        );
        Assert.Equal(
            [
                new(""),
                new("変数1"),
                new("変数2"),
                new("変数3")
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
                new(new(1), new("スイッチ1"), new(true)),
                new(new(2), new("スイッチ2"), new(false)),
                new(new(3), new("スイッチ3"), new(null)),
            ],
            actual.Switches
        );
        Assert.Equal(
            [
                new(new(1), new("変数1"), new(1)),
                new(new(2), new("変数2"), new("a")),
                new(new(3), new("変数3"), new(null)),
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
                new(new(new(1), new("アイテム1"), new("アイテム1の説明")), 1),
                new(new(new(2), new("アイテム2"), new("アイテム2の説明")), 2),
            ],
            actual.HeldItems
        );
        Assert.Equal(
            [
                new(new(new(1), new("武器1"), new("武器1の説明")), 3),
                new(new(new(2), new("武器2"), new("武器2の説明")), 4),
            ],
            actual.HeldWeapons
        );
        Assert.Equal(
            [
                new(new(new(1), new("防具1"), new("防具1の説明")), 5),
                new(new(new(2), new("防具2"), new("防具2の説明")), 6),
            ],
            actual.HeldArmors
        );
    }
}

// Arrange

// Action

// Assert
