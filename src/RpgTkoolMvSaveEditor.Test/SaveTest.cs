using LZStringCSharp;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Armors;
using RpgTkoolMvSaveEditor.Model.GameDatas.Systems;
using RpgTkoolMvSaveEditor.Model.Items;
using RpgTkoolMvSaveEditor.Model.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Weapons;

namespace RpgTkoolMvSaveEditor.Test;

public class SaveTest
{
    private readonly WwwContext wwwContext_ = new();

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
                new(new(1), new("スイッチ1"), new(false)),
                new(new(2), new("スイッチ2"), new(null)),
                new(new(3), new("スイッチ3"), new(true)),
                new(new(4), new(""), new(null)),
                new(new(5), new(""), new(false)),
                new(new(6), new(""), new(true)),
            ],
            [
                new(new(1), new("変数1"), new("a")),
                new(new(2), new("変数2"), new(null)),
                new(new(3), new("変数3"), new(1)),
                new(new(4), new(""), new(null)),
                new(new(5), new(""), new(true)),
                new(new(6), new(""), new(false)),
            ],
            [
                new("Alice", 999, 999, 999, 99, 999999),
                null,
                new("Michael", 999, 999, 999, 99, 999999),
            ],
            new(999999),
            [
                new(new(new(1), new("アイテム1"), new("アイテム1の説明")), 99),
                new(new(new(2), new("アイテム2"), new("アイテム2の説明")), 99),
                new(new(new(3), new(""), new("")), 99),
            ],
            [
                new(new(new(1), new("武器1"), new("武器1の説明")), 99),
                new(new(new(2), new("武器2"), new("武器2の説明")), 99),
                new(new(new(3), new(""), new("")), 99),
            ],
            [
                new(new(new(1), new("防具1"), new("防具1の説明")), 99),
                new(new(new(2), new("防具2"), new("防具2の説明")), 99),
                new(new(new(3), new(""), new("")), 99),
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
}

// Arrange

// Action

// Assert
