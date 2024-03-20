using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class ItemVM(Item item) : NotificationObject
{
    #region Binding Property

    private int id_ = item.Id;
    private string name_ = item.Name;
    private string description_ = item.Description;
    private int count_ = item.Count;

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
}