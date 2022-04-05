using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class CommonDataControlVM : NotifycationObject
{
    #region Binding Property

    private ObservableCollection<Switch> switches_ = new();

    public ObservableCollection<Switch> Switches { get => switches_; set => SetProperty(ref switches_, value); }

    #endregion Binding Property

    public CommonDataControlVM()
    {
        Dependency.App.CommonDataLoaded += (s, e) =>
        {

        }
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

    #endregion Binding Property
}