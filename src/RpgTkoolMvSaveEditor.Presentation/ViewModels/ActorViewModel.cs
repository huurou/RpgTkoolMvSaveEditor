using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using RpgTkoolMvSaveEditor.Model.GameData;
using RpgTkoolMvSaveEditor.Presentation.Messages;

namespace RpgTkoolMvSaveEditor.Presentation.ViewModels;

public partial class ActorViewModel(Actor model) : ObservableObject
{
    [ObservableProperty] private int hP = model.HP;
    [ObservableProperty]
    private int mP = model.MP;
    [ObservableProperty]
    private int tP = model.TP;
    [ObservableProperty]
    private int level = model.Level;
    [ObservableProperty]
    private int exp = model.Exp;

    public int Id { get; } = model.Id;
    public string Name { get; } = model.Name;

    public Actor ToModel()
    {
        return new(Id, Name, HP, MP, TP, Level, Exp);
    }

    partial void OnHPChanged(int value)
    {
        WeakReferenceMessenger.Default.Send(new SaveDataChangedMessage());
    }

    partial void OnMPChanged(int value)
    {
        WeakReferenceMessenger.Default.Send(new SaveDataChangedMessage());
    }

    partial void OnTPChanged(int value)
    {
        WeakReferenceMessenger.Default.Send(new SaveDataChangedMessage());
    }

    partial void OnLevelChanged(int value)
    {
        WeakReferenceMessenger.Default.Send(new SaveDataChangedMessage());
    }

    partial void OnExpChanged(int value)
    {
        WeakReferenceMessenger.Default.Send(new SaveDataChangedMessage());
    }
}
