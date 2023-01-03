using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class ArmorVM : NotificationObject
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
            Dependency.App.SetSaveDataArmor(Id.ToString(), value);
            SetProperty(ref count_, value);
        }
    }

    #endregion Binding Property

    public ArmorVM(Armor armor)
    {
        id_ = armor.Id;
        name_ = armor.Name;
        description_ = armor.Description;
        count_ = armor.Count;
    }
}