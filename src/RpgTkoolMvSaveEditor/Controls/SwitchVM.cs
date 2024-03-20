using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class SwitchVM(Switch @switch) : NotificationObject
{
    #region Binding Property

    private int id_ = @switch.Id;
    private string name_ = @switch.Name;
    private bool? value_ = @switch.Value;

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
}