using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using RpgTkoolMvSaveEditor.Presentation.Messages;

namespace RpgTkoolMvSaveEditor.Presentation.ViewModels;

public partial class GoldViewModel(int value = default) : ObservableObject
{
    [ObservableProperty] private int value = value;

    partial void OnValueChanged(int value)
    {
        WeakReferenceMessenger.Default.Send(new SaveDataChangedMessage());
    }
}
