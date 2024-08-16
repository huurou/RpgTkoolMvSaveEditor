using Moq;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Model.Settings;

namespace RpgTkoolMvSaveEditor.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var settingRepository = new Mock<ISettingRepository>();
        settingRepository.Setup(x => x.Load()).Returns(Setting.Default);

        // Action

        // Assert
        Assert.Equal("RpgTkoolMvSaveEditor", AppInfo.Name);
    }
}

// Arrange

// Action

// Assert
