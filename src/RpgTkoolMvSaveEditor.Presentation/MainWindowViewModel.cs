using Reactive.Bindings;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs.Services;

namespace RpgTkoolMvSaveEditor.Presentation;

public class MainWindowViewModel : ViewModelBase
{
    public ReactivePropertySlim<string> Title { get; } = new(AppInfo.Name);

    public ReactiveCommand ShowAboutDialogCmd { get; } = new();

    public MainWindowViewModel(DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService, ApplicationService appService)
    {
        ShowAboutDialogCmd.Subscribe(() => aboutDialogService.ShowDialog());
    }
}
