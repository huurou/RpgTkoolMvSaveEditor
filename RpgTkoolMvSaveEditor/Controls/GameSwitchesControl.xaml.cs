using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// GameSwitchesControl.xaml の相互作用ロジック
/// </summary>
public partial class GameSwitchesControl : UserControl
{
    #region Dependency Property

    internal ObservableCollection<GameSwitchVM> Source
    {
        get => (ObservableCollection<GameSwitchVM>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ObservableCollection<GameSwitchVM>), typeof(GameSwitchesControl), new FrameworkPropertyMetadata(default(ObservableCollection<GameSwitchVM>)));

    #endregion Dependency Property

    public GameSwitchesControl()
    {
        InitializeComponent();
    }

    private void MenuItem_SetTrue_Click(object sender, RoutedEventArgs e)
    {
        foreach (GameSwitchVM sw in DataGrid_Switches.SelectedItems)
        {
            sw.Value = true;
        }
    }

    private void MenuItem_SetFalse_Click(object sender, RoutedEventArgs e)
    {
        foreach (GameSwitchVM sw in DataGrid_Switches.SelectedItems)
        {
            sw.Value = false;
        }
    }
}