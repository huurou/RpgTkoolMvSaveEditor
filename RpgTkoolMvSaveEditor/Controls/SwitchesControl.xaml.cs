using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// SwitchesControl.xaml の相互作用ロジック
/// </summary>
public partial class SwitchesControl : UserControl
{
    #region Dependency Property

    internal ObservableCollection<SwitchVM> Source
    {
        get => (ObservableCollection<SwitchVM>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ObservableCollection<SwitchVM>), typeof(SwitchesControl), new FrameworkPropertyMetadata(default(ObservableCollection<SwitchVM>)));

    #endregion Dependency Property

    public SwitchesControl()
    {
        InitializeComponent();
    }

    private void MenuItem_SetTrue_Click(object sender, RoutedEventArgs e)
    {
        foreach (SwitchVM sw in DataGrid_Switches.SelectedItems)
        {
            sw.Value = true;
        }
    }

    private void MenuItem_SetFalse_Click(object sender, RoutedEventArgs e)
    {
        foreach (SwitchVM sw in DataGrid_Switches.SelectedItems)
        {
            sw.Value = false;
        }
    }
}