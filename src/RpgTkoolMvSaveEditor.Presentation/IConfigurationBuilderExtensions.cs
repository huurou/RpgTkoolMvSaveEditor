using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Presentation;

public static class ConfigurationExtensions
{
    [Conditional("DEBUG")]
    public static void AddSettingsJsonForDebug(this IConfigurationBuilder builder)
    {
        builder.AddJsonFile("appsettings.Debug.json");
    }
}
