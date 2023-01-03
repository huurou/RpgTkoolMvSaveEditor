using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// ItemsControl.xaml の相互作用ロジック
/// </summary>
public partial class ItemsControl : UserControl
{
    #region Dependency Property

    internal ObservableCollection<ItemVM> Source
    {
        get => (ObservableCollection<ItemVM>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ObservableCollection<ItemVM>), typeof(ItemsControl), new FrameworkPropertyMetadata(default(ObservableCollection<ItemVM>)));

    #endregion Dependency Property

    public ItemsControl()
    {
        InitializeComponent();
    }

    private void MenuItem_Set1_Click(object sender, RoutedEventArgs e)
    {
        foreach (ItemVM item in DataGrid_Items.SelectedItems)
        {
            item.Count = 1;
        }
    }

    private void MenuItem_Set99_Click(object sender, RoutedEventArgs e)
    {
        foreach (ItemVM item in DataGrid_Items.SelectedItems)
        {
            item.Count = 99;
        }
    }

    private void MenuItem_SetCount_Click(object sender, RoutedEventArgs e)
    {
    }
}