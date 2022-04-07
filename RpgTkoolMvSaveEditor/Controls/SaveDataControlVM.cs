using RpgTkoolMvSaveEditor.Application;
using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class SaveDataControlVM : NotifycationObject
{
    #region Binding Property

    private ObservableCollection<GameSwitchVM> switches_ = new();
    public ObservableCollection<GameSwitchVM> Switches { get => switches_; set => SetProperty(ref switches_, value); }

    private ObservableCollection<GameVariableVM> variables_ = new();
    public ObservableCollection<GameVariableVM> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    #endregion Binding Property

    public SaveDataControlVM(IEnumerable<GameSwitch> switches, IEnumerable<GameVariable> variables)
    {
        Switches = new(switches.Select(sw => new GameSwitchVM(sw)));
        Variables = new(variables.Select(va => new GameVariableVM(va)));
    }
}