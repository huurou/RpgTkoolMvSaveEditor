using System.Collections.ObjectModel;

namespace RpgTkoolMvSaveEditor.Controls;

internal class SaveDataTabControlVM : NotifycationObject
{
    #region Binding Property

    private ObservableCollection<SaveDataVM> saveDatas_ = new();
    public ObservableCollection<SaveDataVM> SaveDatas { get => saveDatas_; set => SetProperty(ref saveDatas_, value); }

    #endregion Binding Property

    public SaveDataTabControlVM()
    {
        Dependency.App.SaveDataListLoaded += (s, e) => SaveDatas = new(e.Select(x => new SaveDataVM(x.fileName, new(x.switches, x.variables))));
    }
}
