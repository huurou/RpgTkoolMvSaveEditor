using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Queries;
using RpgTkoolMvSaveEditor.Model.Queries.Common;

namespace RpgTkoolMvSaveEditor.Test;

public class LoadTest
{
    private readonly IServiceCollection services_;
    private readonly IServiceProvider provider_;

    public LoadTest()
    {
        services_ = new ServiceCollection();
        services_.AddSingleton<IQueryHandler<GetSaveDataQuery, SaveDataViewDto>, GetSaveDataQueryHandler>();
        services_.AddSingleton<Context>(_ => new([Path.Combine("loadTestData", "www")]));
        services_.AddSingleton<SaveDataJsonNodeStore>();
        services_.AddSingleton<SystemDataLoader>();
        services_.AddSingleton<IQueryHandler<GetCommonSaveDataQuery, CommonSaveDataViewDto>, GetCommonSaveDataQueryHandler>();
        services_.AddSingleton<CommonSaveDataJsonNodeStore>();
        services_.AddLogging(b => b.AddProvider(NullLoggerProvider.Instance));

        provider_ = services_.BuildServiceProvider();
    }

    [Fact]
    public async Task SaveDataロード()
    {
        // Arrange
        var handler = provider_.GetRequiredService<IQueryHandler<GetSaveDataQuery, SaveDataViewDto>>();

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
            actual.Items
        );
        Assert.Equal(
            [
                new(1, "武器1", "武器1の説明", 3),
                new(2, "武器2", "武器2の説明", 4),
            ],
            actual.Weapons
        );
        Assert.Equal(
            [
                new(1, "防具1", "防具1の説明", 5),
                new(2, "防具2", "防具2の説明", 6),
            ],
            actual.Armors
        );
    }

    [Fact]
    public async Task CommonSaveDataロード()
    {
        // Arrange
        var handler = provider_.GetRequiredService<IQueryHandler<GetCommonSaveDataQuery, CommonSaveDataViewDto>>();

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
