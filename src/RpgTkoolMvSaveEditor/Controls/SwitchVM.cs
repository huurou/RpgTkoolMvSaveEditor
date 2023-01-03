using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class SwitchVM : NotificationObject
{
    #region Binding Property

    private int id_;
    private string name_;
    private bool? value_;

    public int Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public bool? Value
    {
        get => value_;
        set
        {
            Dependency.App.SetSaveDataSwitch(Id, value);
            SetProperty(ref value_, value);
        }
    }

    #endregion Binding Property

    public SwitchVM(Switch @switch)
    {
        id_ = @switch.Id;
        name_ = @switch.Name;
        value_ = @switch.Value;
    }
}