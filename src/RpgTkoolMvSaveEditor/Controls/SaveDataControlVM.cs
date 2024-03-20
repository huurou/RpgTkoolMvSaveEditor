using System.Collections.ObjectModel;
using System.Linq;

namespace RpgTkoolMvSaveEditor.Controls;

internal class SaveDataControlVM : NotificationObject
{
    #region Binding Property

    private ParametersVM? parameters_;
    private ObservableCollection<SwitchVM> switches_ = [];
    private ObservableCollection<VariableVM> variables_ = [];
    private ObservableCollection<ItemVM> items_ = [];
    private ObservableCollection<WeaponVM> weapons_ = [];
    private ObservableCollection<ArmorVM> armors_ = [];
    private ObservableCollection<ActorVM> actros_ = [];

    public ParametersVM? Parameters { get => parameters_; set => SetProperty(ref parameters_, value); }
    public ObservableCollection<SwitchVM> Switches { get => switches_; set => SetProperty(ref switches_, value); }
    public ObservableCollection<VariableVM> Variables { get => variables_; set => SetProperty(ref variables_, value); }
    public ObservableCollection<ItemVM> Items { get => items_; set => SetProperty(ref items_, value); }
    public ObservableCollection<WeaponVM> Weapons { get => weapons_; set => SetProperty(ref weapons_, value); }
    public ObservableCollection<ArmorVM> Armors { get => armors_; set => SetProperty(ref armors_, value); }
    public ObservableCollection<ActorVM> Actors { get => actros_; set => SetProperty(ref actros_, value); }

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
            Actors = new(e.actors.Select(x => new ActorVM(x)));
        };
    }
}