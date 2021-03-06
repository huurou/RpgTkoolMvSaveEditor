using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class GameVariableVM : NotificationObject
{
    #region Binding Property

    private string id_;
    private string name_;
    private object? value_;

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

    public GameVariableVM(GameVariable variable)
    {
        id_ = variable.Id;
        name_ = variable.Name;
        value_ = variable.Value;
    }
}