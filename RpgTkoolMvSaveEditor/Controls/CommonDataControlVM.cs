using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotifycationObject
{
    public event EventHandler<(Action<IEnumerable<SwitchVM>, bool> action, bool value)>? SetValueSelectedSwitches;

    #region Binding Property

    private ObservableCollection<SwitchVM>? switches_;
    private ObservableCollection<VariableVM>? variables_;

    public ObservableCollection<SwitchVM>? Switches { get => switches_; set => SetProperty(ref switches_, value); }
    public ObservableCollection<VariableVM>? Variables { get => variables_; set => SetProperty(ref variables_, value); }

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
            Switches = new ObservableCollection<SwitchVM>(e.switches.Select(x => new SwitchVM(x)));
            Variables = new ObservableCollection<VariableVM>(e.variables.Select(x => new VariableVM(x)));
        };
    }
}