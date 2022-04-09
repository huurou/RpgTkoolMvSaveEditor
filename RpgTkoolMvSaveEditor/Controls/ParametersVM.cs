using RpgTkoolMvSaveEditor.Application;

namespace RpgTkoolMvSaveEditor.Controls;

internal class ParametersVM : NotifycationObject
{
    #region Binding Property

    private int gold_;
    public int Gold
    {
        get => gold_;
        set
        {
            Dependency.App.SetSaveDataGold(value);
            SetProperty(ref gold_, value);
        }
    }

    #endregion Binding Property

    public ParametersVM(Parameters? parameters)
    {
        if (parameters is null) return;
        gold_ = parameters.Gold;
    }
}