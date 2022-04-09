using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class SaveDataControlVM : NotifycationObject
{
    #region Binding Property

    private ParametersVM? parameters_;
    public ParametersVM? Parameters { get => parameters_; set => SetProperty(ref parameters_, value); }

    private ObservableCollection<SwitchVM> switches_ = new();
    public ObservableCollection<SwitchVM> Switches { get => switches_; set => SetProperty(ref switches_, value); }

    private ObservableCollection<VariableVM> variables_ = new();
    public ObservableCollection<VariableVM> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    private ObservableCollection<ItemVM> items_ = new();
    public ObservableCollection<ItemVM> Items { get => items_; set => SetProperty(ref items_, value); }

    private ObservableCollection<WeaponVM> weapons_=new();
    public ObservableCollection<WeaponVM> Weapons { get => weapons_; set => SetProperty(ref weapons_, value); }

    private ObservableCollection<ArmorVM> armors_ = new();
    public ObservableCollection<ArmorVM> Armors { get => armors_; set => SetProperty(ref armors_, value); }

    #endregion Binding Property

    public SaveDataControlVM()
    {
        Dependency.App.SaveDataLoaded += (s, e) =>
        {
            Parameters = new(e.parameters);
            Switches = new(e.switches.Select(x => new SwitchVM(x)));
            Variables = new(e.variables.Select(x => new VariableVM(x)));
            Items = new(e.items.Select(x => new ItemVM(x)));
            Weapons = new(e.weapons.Select(x => new WeaponVM(x)));
            Armors = new(e.armors.Select(x => new ArmorVM(x)));
        };
    }
}