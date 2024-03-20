using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class ArmorVM(Armor armor) : NotificationObject
{
    #region Binding Property

    private int id_ = armor.Id;
    private string name_ = armor.Name;
    private string description_ = armor.Description;
    private int count_ = armor.Count;

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
}