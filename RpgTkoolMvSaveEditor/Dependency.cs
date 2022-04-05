using Microsoft.Extensions.DependencyInjection;
using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor;

internal static class Dependency
{
    internal static ApplicationService App { get; }

    static Dependency()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ApplicationService>();

        using var provider = services.BuildServiceProvider();
        App = provider.GetRequiredService<ApplicationService>();
    }
}