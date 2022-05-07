using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotificationObject
{
    #region Binding Property

    private ObservableCollection<GameSwitchVM> switches_ = new();
    private ObservableCollection<GameVariableVM> variables_ = new();

    public ObservableCollection<GameSwitchVM> Switches { get => switches_; set => SetProperty(ref switches_, value); }
    public ObservableCollection<GameVariableVM> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    #endregion Binding Property

    public CommonDataControlVM()
    {
        Dependency.App.CommonDataLoaded += (s, e) =>
        {
            Switches = new(e.switches.Select(x => new GameSwitchVM(x)));
            Variables = new(e.variables.Select(x => new GameVariableVM(x)));
        };
    }
}