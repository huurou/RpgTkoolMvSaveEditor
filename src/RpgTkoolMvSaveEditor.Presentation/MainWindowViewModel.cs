using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
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
    [ObservableProperty] private List<ItemViewModel> selectedItems = [];
    [ObservableProperty] private List<WeaponViewModel> weapons = [];
    [ObservableProperty] private List<WeaponViewModel> selectedWeapons = [];
    [ObservableProperty] private List<ArmorViewModel> armors = [];
    [ObservableProperty] private List<ArmorViewModel> selectedArmors = [];
    [ObservableProperty] private int gold;
    [ObservableProperty] private List<ActorViewModel> actors = [];
    [ObservableProperty] private int actorsSelectedIndex = -1;

    [RelayCommand]
    public async Task Loaded()
    {
        await appService_.LoadDataAsync();
    }

    [RelayCommand]
    public async Task OpenWwwDirSelectDialog()
    {
        var wwwDirSelectDialog = new OpenFolderDialog { Title = "wwwフォルダを選択してください。", };
        if (wwwDirSelectDialog.ShowDialog() == true)
        {
            await appService_.SelectWwwDirAsync(wwwDirSelectDialog.FolderName);
        }
    }

    [RelayCommand]
    public void ShowAboutDialog()
    {
        aboutDialogService_.ShowDialog();
    }

    [RelayCommand]
    public void Set99Items()
    {
        foreach (var item in SelectedItems)
        {
            item.Count = 99;
        }
    }

    [RelayCommand]
    public void Set99Weapons()
    {
        foreach (var weapon in SelectedWeapons)
        {
            weapon.Count = 99;
        }
    }

    [RelayCommand]
    public void Set99Armors()
    {
        foreach (var armor in SelectedArmors)
        {
            armor.Count = 99;
        }
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
