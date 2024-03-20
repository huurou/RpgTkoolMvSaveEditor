using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class WeaponVM(Weapon weapon) : NotificationObject
{
    #region Binding Property

    private int id_ = weapon.Id;
    private string name_ = weapon.Name;
    private string description_ = weapon.Description;
    private int count_ = weapon.Count;

    public int Id { get => id_; set => SetProperty(ref id_, value); }
    public string Name { get => name_; set => SetProperty(ref name_, value); }
    public string Description { get => description_; set => SetProperty(ref description_, value); }
    public int Count
    {
        get => count_;
        set
        {
            Dependency.App.SetSaveDataWeapon(Id.ToString(), value);
            SetProperty(ref count_, value);
        }
    }

    #endregion Binding Property
}