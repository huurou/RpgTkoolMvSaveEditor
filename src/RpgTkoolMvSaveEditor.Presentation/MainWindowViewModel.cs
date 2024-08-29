using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs.Common;

namespace RpgTkoolMvSaveEditor.Presentation;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService_;
    private readonly ApplicationService appService_;

    [ObservableProperty]
    private string title = AppInfo.Name;

    [RelayCommand]
    public void Loaded()
    {
    }

    [RelayCommand]
    public void ShowAboutDialog()
    {
        aboutDialogService_.ShowDialog();
    }

    public MainWindowViewModel(DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService, ApplicationService appService)
    {
        aboutDialogService_ = aboutDialogService;
        appService_ = appService;
    }
}
