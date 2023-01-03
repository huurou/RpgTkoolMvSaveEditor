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
    private static readonly IServiceCollection services_ = new ServiceCollection();
    private static readonly IServiceProvider provider_;

    internal static ApplicationService App => provider_.GetRequiredService<ApplicationService>();

    static Dependency()
    {
        services_.AddSingleton<ApplicationService>();
        services_.AddTransient<IGameDataLoader, GameDataLoader>();
        services_.AddTransient<ICommonDataLoader, CommonDataLoader>();
        services_.AddTransient<ISaveDataLoader, SaveDataLoader>();
        services_.AddTransient<IDataCtrl, JsonCtrl>();
        services_.AddTransient<ISaveDataCtrl, SaveDataCtrl>();

        provider_ = services_.BuildServiceProvider();
    }
}