using Microsoft.Extensions.DependencyInjection;
using RpgTkoolMvSaveEditor.Application;
using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Infrastructure;

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
        services.AddTransient<ISaveDataConverter, SaveDataConverter>();
        services.AddTransient<IJsonCtrl, JsonCtrl>();

        using var provider = services.BuildServiceProvider();
        App = provider.GetRequiredService<ApplicationService>();
    }
}