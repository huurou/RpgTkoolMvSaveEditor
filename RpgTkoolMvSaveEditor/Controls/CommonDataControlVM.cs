using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotifycationObject
{
    public event EventHandler<(Action<IEnumerable<GameSwitchVM>, bool> action, bool value)>? SetValueSelectedSwitches;

    #region Binding Property

    private ObservableCollection<GameSwitchVM> switches_ = new();
    private ObservableCollection<GameVariableVM> variables_ = new();

    public ObservableCollection<GameSwitchVM> Switches { get => switches_; set => SetProperty(ref switches_, value); }
    public ObservableCollection<GameVariableVM> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    #endregion Binding Property

    #region Binding Command

    private DelegateCommand? setSelectedTrueCmd_;
    private DelegateCommand? setSelectedFalseCmd_;

    public DelegateCommand SetSelectedTrueCmd => setSelectedTrueCmd_ ??= new DelegateCommand(
        () => SetValueSelectedSwitches?.Invoke(this, ((switches, value) =>
            {
                foreach (var sw in switches)
                {
                    sw.Value = value;
                }
            }, true)));
    public DelegateCommand SetSelectedFalseCmd => setSelectedFalseCmd_ ??= new DelegateCommand(
        () => SetValueSelectedSwitches?.Invoke(this, ((switches, value) =>
            {
                foreach (var sw in switches)
                {
                    sw.Value = value;
                }
            }, false)));

    #endregion Binding Command

    public CommonDataControlVM()
    {
        Dependency.App.CommonDataLoaded += (s, e) =>
        {
            Switches = new ObservableCollection<GameSwitchVM>(e.switches.Select(x => new GameSwitchVM(x)));
            Variables = new ObservableCollection<GameVariableVM>(e.variables.Select(x => new GameVariableVM(x)));
        };
    }
}