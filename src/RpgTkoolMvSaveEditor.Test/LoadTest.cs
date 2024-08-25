using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Queries;

namespace RpgTkoolMvSaveEditor.Test;

public class LoadTest
{
    private readonly Context context_ = new();

    public LoadTest()
    {
        context_.WwwDirPath = Path.Combine("loadTestData", "www");
    }

    [Fact]
    public async Task SaveDataロード()
    {
        // Arrange
        var handler = new GetSaveDataQueryHandler(context_, new(), new(context_));

        // Action
        var result = await handler.HandleAsync(new());

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(1, "スイッチ1", true),
                new(2, "スイッチ2", false),
                new(3, "スイッチ3", null),
            ],
            actual.Switches
        );
        Assert.Equal(
            [
                new(1, "変数1", 1),
                new(2, "変数2", "a"),
                new(3, "変数3", true),
            ],
            actual.Variables
        );
        Assert.Equal(
            [
                new(1, "Alice", 100, 200, 300, 10, 1000),
                null,
                new(3, "Bob", 400, 500, 600, 20, 4000),
            ],
            actual.Actors
        );
        Assert.Equal(123456, actual.Gold);
        Assert.Equal(
            [
                new(1, "アイテム1", "アイテム1の説明", 1),
                new(2, "アイテム2", "アイテム2の説明", 2),
            ],
            actual.HeldItems
        );
        Assert.Equal(
            [
                new(1, "武器1", "武器1の説明", 3),
                new(2, "武器2", "武器2の説明", 4),
            ],
            actual.HeldWeapons
        );
        Assert.Equal(
            [
                new(1, "防具1", "防具1の説明", 5),
                new(2, "防具2", "防具2の説明", 6),
            ],
            actual.HeldArmors
        );
    }

    [Fact]
    public async Task CommonSaveDataロード()
    {
        // Arrange
        var handler = new GetCommonSaveDataQueryHandler(context_, new(), new SystemDataLoader(context_));

        // Action
        var result = await handler.HandleAsync(new());

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(1, "スイッチ1", true),
                new(2, "スイッチ2", false),
                new(3, "スイッチ3", null),
            ],
            actual.GameSwitches
        );
        Assert.Equal(
            [
                new(1, "変数1", 1),
                new(2, "変数2", "a"),
                new(3, "変数3", true),
            ],
            actual.GameVariables
        );
    }
}

// Arrange

// Action

// Assert
