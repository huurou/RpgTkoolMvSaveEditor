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
    public async Task ItemsJson���[�h()
    {
        // Arrange
        var itemsLoader = new ItemsLoader(wwwContext_);

        // Action
        var result = await itemsLoader.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), new("�A�C�e��1"), new("�A�C�e��1�̐���")),
                new(new(2), new("�A�C�e��2"), new("�A�C�e��2�̐���")),
            ],
            actual
        );
    }

    [Fact]
    public async Task WeaponsJson���[�h()
    {
        // Arrange
        var weaponsLoader = new WeaponsLoader(wwwContext_);

        // Action
        var result = await weaponsLoader.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), new("����1"), new("����1�̐���")),
                new(new(2), new("����2"), new("����2�̐���")),
            ],
            actual
        );
    }

    [Fact]
    public async Task ArmorsJson���[�h()
    {
        // Arrange
        var armorsLoader = new ArmorsLoader(wwwContext_);

        // Action
        var result = await armorsLoader.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), new("�h��1"), new("�h��1�̐���")),
                new(new(2), new("�h��2"), new("�h��2�̐���")),
            ],
            actual
        );
    }

    [Fact]
    public async Task SystemJson���[�h()
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
                new("�X�C�b�`1"),
                new("�X�C�b�`2"),
                new("�X�C�b�`3")
            ],
            actual.SwitchNames
        );
        Assert.Equal(
            [
                new(""),
                new("�ϐ�1"),
                new("�ϐ�2"),
                new("�ϐ�3")
            ],
            actual.VariableNames
        );
    }

    [Fact]
    public async Task SaveData���[�h()
    {
        // Arrange
        var saveDataRepository = new SaveDataRepository(wwwContext_, new SystemLoader(wwwContext_), new ItemsLoader(wwwContext_), new WeaponsLoader(wwwContext_), new ArmorsLoader(wwwContext_));

        // Action
        var result = await saveDataRepository.LoadAsync();

        // Assert
        Assert.True(result.Unwrap(out var actual, out _));
        Assert.Equal(
            [
                new(new(1), new("�X�C�b�`1"), new(true)),
                new(new(2), new("�X�C�b�`2"), new(false)),
                new(new(3), new("�X�C�b�`3"), new(null)),
            ],
            actual.Switches
        );
        Assert.Equal(
            [
                new(new(1), new("�ϐ�1"), new(1)),
                new(new(2), new("�ϐ�2"), new("a")),
                new(new(3), new("�ϐ�3"), new(null)),
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
                new(new(new(1), new("�A�C�e��1"), new("�A�C�e��1�̐���")), 1),
                new(new(new(2), new("�A�C�e��2"), new("�A�C�e��2�̐���")), 2),
            ],
            actual.HeldItems
        );
        Assert.Equal(
            [
                new(new(new(1), new("����1"), new("����1�̐���")), 3),
                new(new(new(2), new("����2"), new("����2�̐���")), 4),
            ],
            actual.HeldWeapons
        );
        Assert.Equal(
            [
                new(new(new(1), new("�h��1"), new("�h��1�̐���")), 5),
                new(new(new(2), new("�h��2"), new("�h��2�̐���")), 6),
            ],
            actual.HeldArmors
        );
    }
}

// Arrange

// Action

// Assert
