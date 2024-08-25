﻿using LZStringCSharp;
using Microsoft.Extensions.DependencyInjection;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Commands;
using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;

namespace RpgTkoolMvSaveEditor.Test;

public class SaveTest
{
    private readonly IServiceCollection services_;
    private readonly IServiceProvider provider_;

    public SaveTest()
    {
        services_ = new ServiceCollection();
        services_.AddSingleton<ICommandHandler<UpdateSwitchCommand>, UpdateSwitchCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateVariableCommand>, UpdateVariableCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateActorCommand>, UpdateActorCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateGoldCommand>, UpdateGoldCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateItemCommand>, UpdateItemCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateWeaponCommand>, UpdateWeaponCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateArmorCommand>, UpdateArmorCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateCommonSwitchCommand>, UpdateCommonSwitchCommandHandler>();
        services_.AddSingleton<ICommandHandler<UpdateCommonVariableCommand>, UpdateCommonVariableCommandHandler>();
        services_.AddSingleton<Context>(_ => new([Path.Combine("saveTestData", "www")]));
        services_.AddSingleton<SaveDataJsonNodeStore>();
        services_.AddSingleton<CommonSaveDataJsonNodeStore>();

        provider_ = services_.BuildServiceProvider();
    }

    // file1.rpgsave.originalをコピーしてfile1.rpgsaveを作成し、file1.rpgsaveを編集して保存後各expectedファイルと比較する
    [Fact]
    public async Task スイッチ変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateSwitchCommand>>();

        // Action
        await handler.HandleAsync(new UpdateSwitchCommand(1, false));
        await handler.HandleAsync(new UpdateSwitchCommand(2, true));
        await handler.HandleAsync(new UpdateSwitchCommand(3, false));
        await handler.HandleAsync(new UpdateSwitchCommand(4, true));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.switch.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "file1.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    [Fact]
    public async Task 変数変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateVariableCommand>>();

        // Action
        await handler.HandleAsync(new UpdateVariableCommand(1, "a"));
        await handler.HandleAsync(new UpdateVariableCommand(2, 1));
        await handler.HandleAsync(new UpdateVariableCommand(3, null));
        await handler.HandleAsync(new UpdateVariableCommand(4, false));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.variable.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "file1.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    [Fact]
    public async Task アクター変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateActorCommand>>();

        // Action
        await handler.HandleAsync(new UpdateActorCommand(1, "Alice", 999, 999, 999, 99, 999999));
        await handler.HandleAsync(new UpdateActorCommand(3, "Michael", 999, 999, 999, 99, 999999));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.actor.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "file1.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    [Fact]
    public async Task ゴールド変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateGoldCommand>>();

        // Action
        await handler.HandleAsync(new UpdateGoldCommand(999999));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.gold.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "file1.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    [Fact]
    public async Task 所持アイテム数変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateItemCommand>>();

        // Action
        await handler.HandleAsync(new UpdateItemCommand(1, 99));
        await handler.HandleAsync(new UpdateItemCommand(2, 99));
        await handler.HandleAsync(new UpdateItemCommand(3, 99));
        await handler.HandleAsync(new UpdateItemCommand(4, 99));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.item.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "file1.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    [Fact]
    public async Task 所持武器数変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateWeaponCommand>>();

        // Action
        await handler.HandleAsync(new UpdateWeaponCommand(1, 99));
        await handler.HandleAsync(new UpdateWeaponCommand(2, 99));
        await handler.HandleAsync(new UpdateWeaponCommand(3, 99));
        await handler.HandleAsync(new UpdateWeaponCommand(4, 99));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.weapon.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "file1.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    [Fact]
    public async Task 所持防具数変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateArmorCommand>>();

        // Action
        await handler.HandleAsync(new UpdateArmorCommand(1, 99));
        await handler.HandleAsync(new UpdateArmorCommand(2, 99));
        await handler.HandleAsync(new UpdateArmorCommand(3, 99));
        await handler.HandleAsync(new UpdateArmorCommand(4, 99));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.armor.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "file1.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    // common.rpgsave.originalをコピーしてcommon.rpgsaveを作成し、common.rpgsaveを編集して保存後各expectedファイルと比較する
    [Fact]
    public async Task 共通スイッチ変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "common.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "common.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateCommonSwitchCommand>>();

        // Action
        await handler.HandleAsync(new UpdateCommonSwitchCommand(1, false));
        await handler.HandleAsync(new UpdateCommonSwitchCommand(2, true));
        await handler.HandleAsync(new UpdateCommonSwitchCommand(3, false));
        await handler.HandleAsync(new UpdateCommonSwitchCommand(4, true));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "common.expected.switch.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "common.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }

    [Fact]
    public async Task 共通変数変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "common.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "common.rpgsave"), true);
        var handler = provider_.GetRequiredService<ICommandHandler<UpdateCommonVariableCommand>>();

        // Action
        await handler.HandleAsync(new UpdateCommonVariableCommand(1, "a"));
        await handler.HandleAsync(new UpdateCommonVariableCommand(2, 1));
        await handler.HandleAsync(new UpdateCommonVariableCommand(3, "b"));
        await handler.HandleAsync(new UpdateCommonVariableCommand(4, 2));
        await handler.HandleAsync(new UpdateCommonVariableCommand(5, false));

        // Assert
        var expectedFile = Path.Combine("saveTestData", "www", "save", "common.expected.variable.rpgsave");
        var actualFile = Path.Combine("saveTestData", "www", "save", "common.rpgsave");
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(expectedFile)),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(actualFile))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(expectedFile),
            await File.ReadAllTextAsync(actualFile)
        );
    }
}

// Arrange

// Action

// Assert
