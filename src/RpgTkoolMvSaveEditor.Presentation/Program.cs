using RpgTkoolMvSaveEditor;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Model.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Settings;
using RpgTkoolMvSaveEditor.Presentation;
using RpgTkoolMvSaveEditor.Presentation.Dialogs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs.Common;
using RpgTkoolMvSaveEditor.Util;
using RpgTkoolMvSaveEditor.Util.LogDisplays;
using System.Diagnostics;
using System.Windows;

var builder = WpfApplication<App, MainWindow>.CreateBuilder(args);
builder.Services.AddSingleton<MainWindowViewModel>();
builder.Services.AddTransient<AboutDialog>();
builder.Services.AddTransient<AboutDialogViewModel>();
builder.Services.AddSingleton<DialogService<AboutDialog, AboutDialogViewModel>>();
builder.Services.AddSingleton<ApplicationService>();
builder.Services.AddSingleton<PathProvider>(_ => new(args));
builder.Services.AddSingleton<CommonSaveDataLoader>();
builder.Services.AddSingleton<CommonSaveDataSaver>();
builder.Services.AddSingleton<CommonSaveDataJsonObjectProvider>();
builder.Services.AddSingleton<ICommonSaveDataRepository, CommonSaveDataRepository>();
builder.Services.AddSingleton<SaveDataLoader>();
builder.Services.AddSingleton<SaveDataSaver>();
builder.Services.AddSingleton<SaveDataJsonObjectProvider>();
builder.Services.AddSingleton<ISaveDataRepository, SaveDataRepository>();
builder.Services.AddSingleton<ISettingRepository, SettingRepository>();
builder.Services.AddSingleton<ILogDisplay, LogDisplay>();
builder.Services.AddLogging(b => NLogConfiguration.Configure(Paths.LogsDir, b));
builder.Services.AddSingleton<ILoggerProvider, LogDisplayLoggerProvider>();

var app = builder.Build();

Run(app);

static void Run(WpfApplication<App, MainWindow> app)
{
    // DEBUG時のみ実行される
    OnDebug(app);
    // RELEASE時のみ実行される
    OnRelease(app);

    [Conditional("DEBUG")]
    static void OnDebug(WpfApplication<App, MainWindow> app)
    {
        try
        {
            app.Run();
        }
        catch (Exception ex)
        {
            app.Logger.LogError("{ex}", ex);
            throw;
        }
    }

    [Conditional("RELEASE")]
    static void OnRelease(WpfApplication<App, MainWindow> app)
    {
        try
        {
            app.Run();
        }
        catch (Exception ex)
        {
            app.Logger.LogError("{ex}", ex);
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}