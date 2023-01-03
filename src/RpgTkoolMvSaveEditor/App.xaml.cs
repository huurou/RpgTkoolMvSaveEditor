using System;

namespace RpgTkoolMvSaveEditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static string[] CommandArgs { get; private set; } = Array.Empty<string>();

    private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
    {
        CommandArgs = e.Args;
    }
}