using Reactive.Bindings;
using RpgTkoolMvSaveEditor.Model;
using RpgTkoolMvSaveEditor.Model.Configs;
using RpgTkoolMvSaveEditor.Model.GameData.Armors;
using RpgTkoolMvSaveEditor.Model.GameData.Items;
using RpgTkoolMvSaveEditor.Model.GameData.Switches;
using RpgTkoolMvSaveEditor.Model.GameData.Variables;
using RpgTkoolMvSaveEditor.Model.GameData.Weapons;
using RpgTkoolMvSaveEditor.Presentation.Dialogs;
using RpgTkoolMvSaveEditor.Presentation.Dialogs.Services;

namespace RpgTkoolMvSaveEditor.Presentation;

public class MainWindowViewModel : ViewModelBase
{
    public ReactivePropertySlim<string> Title { get; } = new(AppInfo.Name);
    public ReactivePropertySlim<List<GameSwitchViewModel>> GameSwiches { get; } = new();
    public ReactivePropertySlim<List<GameVariableViewModel>> GameVariables { get; } = new();
    public ReactivePropertySlim<List<SwitchViewModel>> Swiches { get; } = new();
    public ReactivePropertySlim<List<VariableViewModel>> Variables { get; } = new();
    public ReactivePropertySlim<List<ItemViewModel>> Items { get; } = new();
    public ReactivePropertySlim<List<WeaponViewModel>> Weapons { get; } = new();
    public ReactivePropertySlim<List<ArmorViewModel>> Armors { get; } = new();
    public ReactivePropertySlim<GoldViewModel> Gold { get; } = new();

    public ReactiveCommand LoadedCmd { get; } = new();
    public ReactiveCommand ShowAboutDialogCmd { get; } = new();

    public MainWindowViewModel(Context context, DialogService<AboutDialog, AboutDialogViewModel> aboutDialogService, ApplicationService appService, ILogger<MainWindowViewModel> logger)
    {
        LoadedCmd.Subscribe(async () =>
        {
            if (context.WwwDirPath is not null)
            {
                await appService.SelectWwwDirAsync(context.WwwDirPath);
            }
        });
        ShowAboutDialogCmd.Subscribe(() => aboutDialogService.ShowDialog());

        appService.SaveDataLoaded.Subscribe(
            e =>
            {
                Swiches.Value = [.. e.SaveData.Switches.Select(x => new SwitchViewModel(x, appService))];
                Variables.Value = [.. e.SaveData.Variables.Select(x => new VariableViewModel(x, appService))];
                Items.Value = [.. e.SaveData.Items.Select(x => new ItemViewModel(x, appService))];
                Weapons.Value = [.. e.SaveData.Weapons.Select(x => new WeaponViewModel(x, appService))];
                Armors.Value = [.. e.SaveData.Armors.Select(x => new ArmorViewModel(x, appService))];
                Gold.Value = new(e.SaveData.Gold, appService);
            }
        );
        appService.CommonSaveDataLoaded.Subscribe(
            e =>
            {
                GameSwiches.Value = [.. e.CommonSaveData.GameSwitches.Select(x => new GameSwitchViewModel(x, appService))];
                GameVariables.Value = [.. e.CommonSaveData.GameVariables.Select(x => new GameVariableViewModel(x, appService))];
            }
        );
        appService.ErrorOccurred.Subscribe(e => logger.LogError("{}", e.Messaga));
    }
}

public class GameSwitchViewModel : ViewModelBase
{
    public int Id { get; }
    public string Name { get; }
    public ReactivePropertySlim<bool?> Value { get; } = new(mode: ReactivePropertyMode.DistinctUntilChanged);

    public GameSwitchViewModel(SwitchViewDto dto, ApplicationService appService)
    {
        Id = dto.Id;
        Name = dto.Name;
        Value.Value = dto.Value;

        Value.Subscribe(async x => await appService.UpdateGameSwitchAsync(new(Id, x)));
    }
}

public class GameVariableViewModel : ViewModelBase
{
    public int Id { get; }
    public string Name { get; }
    public ReactivePropertySlim<object?> Value { get; } = new(mode: ReactivePropertyMode.DistinctUntilChanged);

    public GameVariableViewModel(VariableViewDto dto, ApplicationService appService)
    {
        Id = dto.Id;
        Name = dto.Name;
        Value.Value = dto.Value;

        Value.Subscribe(async x => await appService.UpdateGameVariableAsync(new(Id, x)));
    }
}

public class SwitchViewModel : ViewModelBase
{
    public int Id { get; }
    public string Name { get; }
    public ReactivePropertySlim<bool?> Value { get; } = new(mode: ReactivePropertyMode.DistinctUntilChanged);

    public SwitchViewModel(SwitchViewDto dto, ApplicationService appService)
    {
        Id = dto.Id;
        Name = dto.Name;
        Value.Value = dto.Value;

        Value.Subscribe(async x => await appService.UpdateSwitchAsync(new(Id, x)));
    }
}

public class VariableViewModel : ViewModelBase
{
    public int Id { get; }
    public string Name { get; }
    public ReactivePropertySlim<object?> Value { get; } = new(mode: ReactivePropertyMode.DistinctUntilChanged);

    public VariableViewModel(VariableViewDto dto, ApplicationService appService)
    {
        Id = dto.Id;
        Name = dto.Name;
        Value.Value = dto.Value;

        Value.Subscribe(async x => await appService.UpdateVariableAsync(new(Id, x)));
    }
}

public class ItemViewModel : ViewModelBase
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public ReactivePropertySlim<int> Count { get; } = new(mode: ReactivePropertyMode.DistinctUntilChanged);

    public ItemViewModel(ItemViewDto dto, ApplicationService appService)
    {
        Id = dto.Id;
        Name = dto.Name;
        Description = dto.Description;
        Count.Value = dto.Count;

        Count.Subscribe(async x => await appService.UpdateItemAsync(new(Id, x)));
    }
}

public class WeaponViewModel : ViewModelBase
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public ReactivePropertySlim<int> Count { get; } = new(mode: ReactivePropertyMode.DistinctUntilChanged);

    public WeaponViewModel(WeaponViewDto dto, ApplicationService appService)
    {
        Id = dto.Id;
        Name = dto.Name;
        Description = dto.Description;
        Count.Value = dto.Count;

        Count.Subscribe(async x => await appService.UpdateWeaponAsync(new(Id, x)));
    }
}

public class ArmorViewModel : ViewModelBase
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public ReactivePropertySlim<int> Count { get; } = new(mode: ReactivePropertyMode.DistinctUntilChanged);

    public ArmorViewModel(ArmorViewDto dto, ApplicationService appService)
    {
        Id = dto.Id;
        Name = dto.Name;
        Description = dto.Description;
        Count.Value = dto.Count;

        Count.Subscribe(async x => await appService.UpdateArmorAsync(new(Id, x)));
    }
}

public class GoldViewModel : ViewModelBase
{
    public ReactivePropertySlim<int> Value { get; } = new();

    public GoldViewModel(int value, ApplicationService appService)
    {
        Value.Value = value;

        Value.Subscribe(async x => await appService.UpdateGoldAsync(new(x)));
    }
}
