using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class GameSwitchVM : NotifycationObject
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

    public GameSwitchVM(GameSwitch data)
    {
        id_ = data.Id;
        name_ = data.Name;
        value_ = data.Value;
    }

    #endregion Binding Property
}
