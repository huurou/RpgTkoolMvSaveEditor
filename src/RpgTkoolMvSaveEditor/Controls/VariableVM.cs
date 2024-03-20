using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class VariableVM(Variable variable) : NotificationObject
{
    #region Binding Property

    private int id_ = variable.Id;
    private string name_ = variable.Name;
    private object? value_ = variable.Value;

    public int Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public object? Value
    {
        get => value_;
        set
        {
            Dependency.App.SetSaveDataVariable(Id, value);
            SetProperty(ref value_, value);
        }
    }

    #endregion Binding Property
}