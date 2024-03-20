using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class GameSwitchVM(GameSwitch data) : NotificationObject
{
    #region Binding Property

    private string id_ = data.Id;
    private string name_ = data.Name;
    private bool? value_ = data.Value;

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

    #endregion Binding Property
}