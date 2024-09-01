using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs.Common;
using RpgTkoolMvSaveEditor.Presentation.Messages;
using RpgTkoolMvSaveEditor.Presentation.ViewModels;
using System.Windows;

namespace RpgTkoolMvSaveEditor.Presentation;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string title = AppInfo.Name;
    [ObservableProperty] private List<GameSwitchViewModel> gameSwitches = [];
    [ObservableProperty] private List<GameVariableViewModel> gameVariables = [];
    [ObservableProperty] private List<SwitchViewModel> switches = [];
    [ObservableProperty] private List<VariableViewModel> variables = [];
    [ObservableProperty] private List<ItemViewModel> items = [];
    [ObservableProperty] private List<WeaponViewModel> weapons = [];
    [ObservableProperty] private List<ArmorViewModel> armors = [];
    [ObservableProperty] private int gold;
    [ObservableProperty] private List<ActorViewModel> actors = [];
    [ObservableProperty] private int actorsSelectedIndex = -1;

    [RelayCommand]
    public async Task Loaded()
    {
        await appService_.LoadDataAsync();
    }

    [RelayCommand]
    public void ShowAboutDialog()
    {
        aboutDialogService_.ShowDialog();
    }

    private readonly DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService_;
    private readonly ApplicationService appService_;

    public MainWindowViewModel(DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService, ApplicationService appService, ILoggerFactory loggerFactory)
    {
        aboutDialogService_ = aboutDialogService;
        appService_ = appService;

        appService.CommonSaveDataLoaded +=
            (s, e) =>
            {
                GameSwitches = [.. e.CommonSaveData.GameSwitches.Select(x => new GameSwitchViewModel(x))];
                GameVariables = [.. e.CommonSaveData.GameVariables.Select(x => new GameVariableViewModel(x))];
            };
        appService.SaveDataLoaded +=
            (s, e) =>
            {
                Switches = [.. e.SaveData.Switches.Select(x => new SwitchViewModel(x))];
                Variables = [.. e.SaveData.Variables.Select(x => new VariableViewModel(x))];
                Items = [.. e.SaveData.Items.Select(x => new ItemViewModel(x))];
                Weapons = [.. e.SaveData.Weapons.Select(x => new WeaponViewModel(x))];
                Armors = [.. e.SaveData.Armors.Select(x => new ArmorViewModel(x))];
                Gold = e.SaveData.Gold;
                Actors = [.. e.SaveData.Actors.Select(x => new ActorViewModel(x))];
                ActorsSelectedIndex = 0;
            };
        appService.ErrorOccurred +=
            (s, e) =>
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                if (s is not null)
                {
                    loggerFactory.CreateLogger(s.GetType()).LogError("{}", e.Message);
                }
                else
                {
                    loggerFactory.CreateLogger<MainWindowViewModel>().LogError("{}", e.Message);
                }
            };

        WeakReferenceMessenger.Default.Register<CommonSaveDataChangedMessage>(
            this,
            async (r, m) => await appService.UpdateCommonSaveDataAsync(
                new(
                    [.. GameSwitches.Select(x => x.ToModel())],
                    [.. GameVariables.Select(x => x.ToModel())]
                )
            )
        );
        WeakReferenceMessenger.Default.Register<SaveDataChangedMessage>(
            this,
            async (r, m) => await appService.UpdateSaveDataAsync(
                new(
                    [.. Switches.Select(x => x.ToModel())],
                    [.. Variables.Select(x => x.ToModel())],
                    Gold,
                    [.. Actors.Select(x => x.ToModel())],
                    [.. Items.Select(x => x.ToModel())],
                    [.. Weapons.Select(x => x.ToModel())],
                    [.. Armors.Select(x => x.ToModel())]
                )
            )
        );
    }
}
