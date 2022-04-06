using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotifycationObject
{
    public event EventHandler<(Action<IEnumerable<Switch>, bool> action, bool value)>? SetValueSelectedSwitches;

    #region Binding Property

    private ObservableCollection<Switch> switches_ = new();
    private ObservableCollection<Variable> variables_ = new();

    public ObservableCollection<Switch> Switches { get => switches_; set => SetProperty(ref switches_, value); }
    public ObservableCollection<Variable> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    #endregion Binding Property

    #region Binding Command

    private DelegateCommand? setSelectedTrueCmd_;
    private DelegateCommand? setSelectedFalseCmd_;

    public DelegateCommand SetSelectedTrueCmd => setSelectedTrueCmd_ ??= new DelegateCommand(() =>
            SetValueSelectedSwitches?.Invoke(this, ((switches, value) =>
            {
                foreach (var sw in switches)
                {
                    sw.Value = value;
                }
            }, true)));
    public DelegateCommand SetSelectedFalseCmd => setSelectedFalseCmd_ ??= new DelegateCommand(() =>
            SetValueSelectedSwitches?.Invoke(this, ((switches, value) =>
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
            Switches = new ObservableCollection<Switch>(e.switches.Select(x => new Switch(x.id, x.name, x.value)));
            Variables = new ObservableCollection<Variable>(e.variables.Select(x => new Variable(x.id, x.name, x.value)));
        };
    }
}

internal class Switch : NotifycationObject
{
    #region Binding Property

    private string id_;
    private string name_;
    private bool? value_;

    public string Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public bool? Value
    {
        get => value_;
        set
        {
            Dependency.App.SetCommonDataSwitch(Id, value);
            SetProperty(ref value_, value);
        }
    }

    public Switch(string id, string name, bool? value)
    {
        id_ = id;
        name_ = name;
        value_ = value;
    }

    #endregion Binding Property
}

internal class Variable : NotifycationObject
{
    #region Binding Property

    private string id_;
    private string name_;
    private object? value_;

    public string Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public object? Value
    {
        get => value_;
        set
        {
            Dependency.App.SetCommonDataVariable(Id, value);
            SetProperty(ref value_, value);
        }
    }

    #endregion Binding Property

    public Variable(string id, string name, object? value)
    {
        id_ = id;
        name_ = name;
        value_ = value;
    }
}