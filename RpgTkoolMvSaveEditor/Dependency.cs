using Microsoft.Extensions.DependencyInjection;
using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor;

internal static class Dependency
{
    public static ApplicationService App { get; }

    static Dependency()
    {
        var services = new ServiceCollection();

        using var provider = services.BuildServiceProvider();
        App = provider.GetService<ApplicationService>() ?? throw new Exception();
    }
}