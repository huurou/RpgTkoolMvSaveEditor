using RpgTkoolMvSaveEditor;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Commands;
using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Queries;
using RpgTkoolMvSaveEditor.Model.Queries.Common;
using RpgTkoolMvSaveEditor.Model.Settings;
using RpgTkoolMvSaveEditor.Presentation;
using RpgTkoolMvSaveEditor.Presentation.Dialogs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs.Services;
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
builder.Services.AddSingleton<Context>(_ => new(args));
builder.Services.AddSingleton<SaveDataAutoLoader>();
builder.Services.AddSingleton<CommonSaveDataAutoLoader>();
builder.Services.AddSingleton<IQueryHandler<GetSaveDataQuery, SaveDataViewDto>, GetSaveDataQueryHandler>();
builder.Services.AddSingleton<IQueryHandler<GetCommonSaveDataQuery, CommonSaveDataViewDto>, GetCommonSaveDataQueryHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateGameSwitchCommand>, UpdateGameSwitchCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateGameVariableCommand>, UpdateGameVariableCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateSwitchCommand>, UpdateSwitchCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateVariableCommand>, UpdateVariableCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateItemCommand>, UpdateItemCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateWeaponCommand>, UpdateWeaponCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateArmorCommand>, UpdateArmorCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<UpdateGoldCommand>, UpdateGoldCommandHandler>();
builder.Services.AddSingleton<SaveDataJsonNodeStore>();
builder.Services.AddSingleton<CommonSaveDataJsonNodeStore>();
builder.Services.AddSingleton<SystemDataLoader>();
builder.Services.AddSingleton<ISettingRepository, SettingRepository>();
builder.Services.AddLogging(b => NLogConfiguration.Configure(Paths.LogsDir, b));

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