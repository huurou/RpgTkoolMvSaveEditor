using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class WeaponVM : NotifycationObject
{
    #region Binding Property

    private int id_;
    private string name_;
    private string description_;
    private int count_;

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

    public WeaponVM(Weapon weapon)
    {
        id_ = weapon.Id;
        name_ = weapon.Name;
        description_ = weapon.Description;
        count_ = weapon.Count;
    }
}