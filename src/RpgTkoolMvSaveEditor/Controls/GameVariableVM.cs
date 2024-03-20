using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class GameVariableVM(GameVariable variable) : NotificationObject
{
    #region Binding Property

    private string id_ = variable.Id;
    private string name_ = variable.Name;
    private object? value_ = variable.Value;

    public string Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public object? Value
    {
        get => value_;
        set
        {
            Dependency.App.SetCommonDataVariable(Id.ToString(), value);
            SetProperty(ref value_, value);
        }
    }

    #endregion Binding Property
}