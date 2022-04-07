using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// GameVariablesControl.xaml の相互作用ロジック
/// </summary>
public partial class GameVariablesControl : UserControl
{
    #region Dependency Property

    internal ObservableCollection<GameVariableVM> Source
    {
        get => (ObservableCollection<GameVariableVM>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ObservableCollection<GameVariableVM>), typeof(GameVariablesControl), new FrameworkPropertyMetadata(default(ObservableCollection<GameVariableVM>)));

    #endregion Dependency Property

    public GameVariablesControl()
    {
        InitializeComponent();
    }
}