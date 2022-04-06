using Microsoft.Extensions.DependencyInjection;
using RpgTkoolMvSaveEditor.Application;
using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.SaveDatas;
using RpgTkoolMvSaveEditor.Infrastructure;
using RpgTkoolMvSaveEditor.Infrastructure.CommonDatas;
using RpgTkoolMvSaveEditor.Infrastructure.GameDatas;
using RpgTkoolMvSaveEditor.Infrastructure.SaveDatas;

namespace RpgTkoolMvSaveEditor;

internal static class Dependency
{
    internal static ApplicationService App { get; }

    static Dependency()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ApplicationService>();
        services.AddTransient<IGameDataLoader, GameDataLoader>();
        services.AddTransient<ICommonDataLoader, CommonDataLoader>();
        services.AddTransient<ISaveDataLoader, SaveDataLoader>();
        services.AddTransient<IJsonCtrl, JsonCtrl>();
        services.AddTransient<ISaveDataCtrl, SaveDataCtrl>();

        using var provider = services.BuildServiceProvider();
        App = provider.GetRequiredService<ApplicationService>();
    }
}