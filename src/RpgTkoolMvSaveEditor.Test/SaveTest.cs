using LZStringCSharp;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Commands;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;

namespace RpgTkoolMvSaveEditor.Test;

public class SaveTest
{
    private readonly Context context_;
    private readonly SaveDataJsonNodeStore saveDataJsonNodeStore_;
    private readonly CommonSaveDataJsonNodeStore commonSaveDataJsonNodeStore_;

    public SaveTest()
    {
        context_ = new Context { WwwDirPath = Path.Combine("saveTestData", "www") };
        saveDataJsonNodeStore_ = new();
        commonSaveDataJsonNodeStore_ = new();
    }

    // file1.rpgsave.originalをコピーしてfile1.rpgsaveを作成し、file1.rpgsaveを編集して保存後各expectedファイルと比較する
    [Fact]
    public async Task スイッチ変更()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var handler = new UpdateSwitchCommandHandler(context_, saveDataJsonNodeStore_);

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
        var handler = new UpdateVariableCommandHandler(context_, saveDataJsonNodeStore_);

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
        var handler = new UpdateActorCommandHandler(context_, saveDataJsonNodeStore_);

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
        var handler = new UpdateGoldCommandHandler(context_, saveDataJsonNodeStore_);

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
        var handler = new UpdateItemCommandHandler(context_, saveDataJsonNodeStore_);

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
        var handler = new UpdateWeaponCommandHandler(context_, saveDataJsonNodeStore_);

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
        var handler = new UpdateArmorCommandHandler(context_, saveDataJsonNodeStore_);

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
        var handler = new UpdateCommonSwitchCommandHandler(context_, commonSaveDataJsonNodeStore_);

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
        var handler = new UpdateCommonVariableCommandHandler(context_, commonSaveDataJsonNodeStore_);

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
