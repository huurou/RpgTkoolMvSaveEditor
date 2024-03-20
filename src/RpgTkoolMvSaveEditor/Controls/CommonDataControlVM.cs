using System.Collections.ObjectModel;
using System.Linq;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotificationObject
{
    #region Binding Property

    private ObservableCollection<GameSwitchVM> switches_ = [];
    private ObservableCollection<GameVariableVM> variables_ = [];

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