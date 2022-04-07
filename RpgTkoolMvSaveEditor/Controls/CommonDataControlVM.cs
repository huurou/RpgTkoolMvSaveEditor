using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotifycationObject
{
    #region Binding Property

    private ObservableCollection<SwitchVM> switches_ = new();
    private ObservableCollection<VariableVM> variables_ = new();

    public ObservableCollection<SwitchVM> Switches { get => switches_; set => SetProperty(ref switches_, value); }
    public ObservableCollection<VariableVM> Variables { get => variables_; set => SetProperty(ref variables_, value); }

    #endregion Binding Property

    public CommonDataControlVM()
    {
        Dependency.App.CommonDataLoaded += (s, e) =>
        {
            Switches = new ObservableCollection<SwitchVM>(e.switches.Select(x => new SwitchVM(x)));
            Variables = new ObservableCollection<VariableVM>(e.variables.Select(x => new VariableVM(x)));
        };

    }
}