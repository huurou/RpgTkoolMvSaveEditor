using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using RpgTkoolMvSaveEditor.Model.GameData;
using RpgTkoolMvSaveEditor.Presentation.Messages;

namespace RpgTkoolMvSaveEditor.Presentation.ViewModels;

public partial class GameSwitchViewModel(Switch model) : ObservableObject
{
    [ObservableProperty] private bool? value = model.Value;

    public int Id { get; } = model.Id;
    public string Name { get; } = model.Name;

    public Switch ToModel()
    {
        return new(Id, Name, Value);
    }

    partial void OnValueChanged(bool? value)
    {
        WeakReferenceMessenger.Default.Send(new CommonSaveDataChangedMessage());
    }
}
