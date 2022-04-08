using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// ArmorsControl.xaml の相互作用ロジック
/// </summary>
public partial class ArmorsControl : UserControl
{
    #region Dependency Property

    internal ObservableCollection<ArmorVM> Source
    {
        get => (ObservableCollection<ArmorVM>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ObservableCollection<ArmorVM>), typeof(ArmorsControl), new FrameworkPropertyMetadata(default(ObservableCollection<ArmorVM>)));

    #endregion Dependency Property

    public ArmorsControl()
    {
        InitializeComponent();
    }

    private void MenuItem_Set1_Click(object sender, RoutedEventArgs e)
    {
        foreach (ArmorVM armor in DataGrid_Items.SelectedItems)
        {
            armor.Count = 1;
        }
    }

    private void MenuItem_Set99_Click(object sender, RoutedEventArgs e)
    {
        foreach (ArmorVM armor in DataGrid_Items.SelectedItems)
        {
            armor.Count = 99;
        }
    }

    private void MenuItem_SetCount_Click(object sender, RoutedEventArgs e)
    {
    }
}