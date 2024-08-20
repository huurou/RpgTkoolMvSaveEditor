using LZStringCSharp;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.CommonDatas;
using RpgTkoolMvSaveEditor.Model.GameDatas.Systems;
using RpgTkoolMvSaveEditor.Model.Items;
using RpgTkoolMvSaveEditor.Model.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Weapons;

namespace RpgTkoolMvSaveEditor.Test;

public class SaveTest
{
    private readonly Context wwwContext_ = new();

    public SaveTest()
    {
        wwwContext_.WwwDirPath = Path.Combine("saveTestData", "www");
    }

    [Fact]
    public async Task SaveDataセーブ()
    {
        // file1.rpgsave.originalをコピーしてfile1.rpgsaveを作成し、file1.rpgsaveを編集して保存後各expectedファイルと比較する

        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var saveDataRepository = new SaveDataRepository(wwwContext_, new SystemLoader(wwwContext_), new ItemsLoader(wwwContext_), new WeaponsLoader(wwwContext_), new ArmorsLoader(wwwContext_));
        var saveData = new SaveData(
            [
                new(new(1), "", false),
                new(new(2), "", null),
                new(new(3), "", true),
                new(new(4), "", null),
                new(new(5), "", false),
                new(new(6), "", true),
            ],
            [
                new(new(1), "", "a"),
                new(new(2), "", null),
                new(new(3), "", 1),
                new(new(4), "", null),
                new(new(5), "", true),
                new(new(6), "", false),
            ],
            [
                new("Alice", 999, 999, 999, 99, 999999),
                null,
                new("Michael", 999, 999, 999, 99, 999999),
            ],
            new(999999),
            [
                new(new(new(1), "", ""), 99),
                new(new(new(2), "", ""), 99),
                new(new(new(3), "", ""), 99),
            ],
            [
                new(new(new(1), "", ""), 99),
                new(new(new(2), "", ""), 99),
                new(new(new(3), "", ""), 99),
            ],
            [
                new(new(new(1), "", ""), 99),
                new(new(new(2), "", ""), 99),
                new(new(new(3), "", ""), 99),
            ]
        );

        // Action
        await saveDataRepository.SaveAsync(saveData);

        // Assert
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "file1.expected.rpgsave"))),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "file1.rpgsave")))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "file1.expected.rpgsave")),
            await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "file1.rpgsave"))
        );
    }

    [Fact]
    public async Task CommonDataセーブ()
    {
        // common.rpgsave.originalをコピーしてcommon.rpgsaveを作成し、common.rpgsaveを編集して保存後各expectedファイルと比較する

        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "common.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "common.rpgsave"), true);
        var commonDataRepository = new CommonDataRepository(wwwContext_, new SystemLoader(wwwContext_));
        var commonData = new CommonData(
            [
                new(new(1), "", false),
                new(new(2), "", null),
                new(new(3), "", true),
            ],
            [
                new(new(1), "", "a"),
                new(new(2), "", true),
                new(new(3), "", null),
                new(new(4), "", 1),
            ]
        );

        // Action
        await commonDataRepository.SaveAsync(commonData);

        // Assert
        var s = LZString.DecompressFromBase64(await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "common.rpgsave")));
        Assert.Equal(
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "common.expected.rpgsave"))),
            LZString.DecompressFromBase64(await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "common.rpgsave")))
        );
        Assert.Equal(
            await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "common.expected.rpgsave")),
            await File.ReadAllTextAsync(Path.Combine("saveTestData", "www", "save", "common.rpgsave"))
        );
    }
}

// Arrange

// Action

// Assert
