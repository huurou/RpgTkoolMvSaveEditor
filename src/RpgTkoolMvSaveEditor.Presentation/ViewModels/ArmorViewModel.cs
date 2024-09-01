using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using RpgTkoolMvSaveEditor.Model.GameData;
using RpgTkoolMvSaveEditor.Presentation.Messages;

namespace RpgTkoolMvSaveEditor.Presentation.ViewModels;

public partial class ArmorViewModel(Armor model) : ObservableObject
{
    [ObservableProperty] private int count = model.Count;

    public int Id { get; } = model.Id;
    public string Name { get; } = model.Name;
    public string Description { get; } = model.Description;

    public Armor ToModel()
    {
        return new(Id, Name, Description, Count);
    }

    partial void OnCountChanged(int value)
    {
        WeakReferenceMessenger.Default.Send(new SaveDataChangedMessage());
    }
}