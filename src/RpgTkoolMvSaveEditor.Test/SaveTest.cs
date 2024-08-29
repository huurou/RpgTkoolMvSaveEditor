using LZStringCSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;

namespace RpgTkoolMvSaveEditor.Test;

public class SaveTest
{
    private readonly IServiceCollection services_ = new ServiceCollection();
    private readonly IServiceProvider provider_;

    public SaveTest()
    {
        services_.AddSingleton<Context>(_ => new([Path.Combine("saveTestData", "www")]));
        services_.AddSingleton<ISaveDataRepository, SaveDataRepository>();
        services_.AddSingleton<ICommonSaveDataRepository, CommonSaveDataRepository>();
        services_.AddLogging(b => b.AddProvider(NullLoggerProvider.Instance));

        provider_ = services_.BuildServiceProvider();
    }

    [Fact]
    public async Task セーブデータセーブAsync()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "file1.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "file1.rpgsave"), true);
        var saveDataRepository = provider_.GetRequiredService<ISaveDataRepository>();
        // 先にrootNodeをロードする あらかじめロードされたrootNodeの一部を書き換える形でセーブするため
        await saveDataRepository.LoadAsync();
        var saveData = new SaveData(
            [
                new(1, "", false),
                new(2, "", null),
                new(3, "", true),
                new(4, "", null),
                new(5, "", false),
                new(6, "", true),
            ],
            [
                new(1, "", "a"),
                new(2, "", null),
                new(3, "", 1),
                new(4, "", null),
                new(5, "", true),
                new(6, "", false),
            ],
            999999,
            [
                new(1,"Alice", 999, 999, 999, 99, 999999),
                new(3,"Michael", 999, 999, 999, 99, 999999),
            ],
            [
                new(1, "", "", 99),
                new(2, "", "", 99),
                new(3, "", "", 99),
            ],
            [
                new(1, "", "", 99),
                new(2, "", "", 99),
                new(3, "", "", 99),
            ],
            [
                new(1, "", "", 99),
                new(2, "", "", 99),
                new(3, "", "", 99),
            ]
        );

        // Action
        var res = await saveDataRepository.SaveAsync(saveData);

        // Assert
        var success = res.Unwrap(out var message);
        if (!success) { throw new Exception(message); }
        var expectedFile = Path.Combine("saveTestData", "www", "save", "file1.expected.rpgsave");
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
    public async Task 共通セーブデータセーブAsync()
    {
        // Arrange
        File.Copy(Path.Combine("saveTestData", "www", "save", "common.original.rpgsave"), Path.Combine("saveTestData", "www", "save", "common.rpgsave"), true);
        var commonSaveDataRepository = provider_.GetRequiredService<ICommonSaveDataRepository>();
        // 先にrootNodeをロードする あらかじめロードされたrootNodeの一部を書き換える形でセーブするため
        await commonSaveDataRepository.LoadAsync();
        var commonSaveData = new CommonSaveData(
            [
                new(1, "", false),
                new(2, "", null),
                new(3, "", true),
            ],
            [
                new(1, "", "a"),
                new(2, "", true),
                new(3, "", null),
                new(4, "", 1),
            ]
        );

        // Action
        var res = await commonSaveDataRepository.SaveAsync(commonSaveData);

        // Assert
        if (!res.Unwrap(out var message)) { throw new Exception(message); }
        var expectedFile = Path.Combine("saveTestData", "www", "save", "common.expected.rpgsave");
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
