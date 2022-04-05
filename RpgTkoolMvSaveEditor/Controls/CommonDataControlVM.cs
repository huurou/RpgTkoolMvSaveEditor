using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotifycationObject
{
    #region Binding Property

    private ObservableCollection<Switch> switches_ = new();
    private ObservableCollection<Variable> variables_ = new();

    public ObservableCollection<Switch> Switches { get => switches_; set => SetProperty(ref switches_, value); }
    public ObservableCollection<Variable> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    #endregion Binding Property

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

    private int id_;
    private string name_ = "";
    private bool value_;

    public int Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public bool Value { get => value_; set => SetProperty(ref value_, value); }

    public Switch(int id, string name, bool value)
    {
        Id = id;
        Name = name;
        Value = value;
    }

    #endregion Binding Property
}

internal class Variable : NotifycationObject
{
    #region Binding Property

    private int id_;
    private string name_ = "";
    private int value_;

    public int Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public int Value { get => value_; set => SetProperty(ref value_, value); }

    #endregion Binding Property

    public Variable(int id, string name, int value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}