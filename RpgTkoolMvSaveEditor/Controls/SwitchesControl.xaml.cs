using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// SwitchesControl.xaml の相互作用ロジック
/// </summary>
public partial class SwitchesControl : UserControl
{
    internal ObservableCollection<GameSwitchVM> Source
    {
        get => (ObservableCollection<GameSwitchVM>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ObservableCollection<GameSwitchVM>), typeof(SwitchesControl), new FrameworkPropertyMetadata(default(ObservableCollection<GameSwitchVM>), (d, e) =>
        {
            if (d is SwitchesControl self &&
                e.NewValue is ObservableCollection<GameSwitchVM> value)
            {
            }
        }));

    public SwitchesControl()
    {
        InitializeComponent();
    }
}