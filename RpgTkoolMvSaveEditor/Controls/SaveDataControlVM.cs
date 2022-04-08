using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class SaveDataControlVM : NotifycationObject
{
    #region Binding Property

    private ObservableCollection<SwitchVM> switches_ = new();
    public ObservableCollection<SwitchVM> Switches { get => switches_; set => SetProperty(ref switches_, value); }

    private ObservableCollection<VariableVM> variables_ = new();
    public ObservableCollection<VariableVM> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    #endregion Binding Property

    public SaveDataControlVM()
    {
        Dependency.App.SaveDataLoaded += (s, e) =>
        {
            Switches = new(e.switches.Select(x => new SwitchVM(x)));
            Variables = new(e.variables.Select(x => new VariableVM(x)));
        };
    }
}