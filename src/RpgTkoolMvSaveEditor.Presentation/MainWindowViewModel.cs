using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs.Common;

namespace RpgTkoolMvSaveEditor.Presentation;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly Context context_;
    private readonly DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService_;
    private readonly ApplicationService appService_;

    [ObservableProperty]
    private string title = AppInfo.Name;

    [RelayCommand]
    public void Loaded()
    {
        if (context_.WwwDirPath is not null)
        {
        }
    }

    [RelayCommand]
    public void ShowAboutDialog()
    {
        aboutDialogService_.ShowDialog();
    }

    public MainWindowViewModel(Context context, DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService, ApplicationService appService)
    {
        context_ = context;
        aboutDialogService_ = aboutDialogService;
        appService_ = appService;
    }
}
