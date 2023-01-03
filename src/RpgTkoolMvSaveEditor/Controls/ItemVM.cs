using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class ItemVM : NotificationObject
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
            Dependency.App.SetSaveDataItem(Id.ToString(), value);
            SetProperty(ref count_, value);
        }
    }

    #endregion Binding Property

    public ItemVM(Item item)
    {
        id_ = item.Id;
        name_ = item.Name;
        description_ = item.Description;
        count_ = item.Count;
    }
}