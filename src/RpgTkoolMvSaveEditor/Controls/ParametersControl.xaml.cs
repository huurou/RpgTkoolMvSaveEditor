using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// ParametersControl.xaml の相互作用ロジック
/// </summary>
public partial class ParametersControl : UserControl
{
    #region Dependency Property

    internal ParametersVM Source
    {
        get => (ParametersVM)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ParametersVM), typeof(ParametersControl), new FrameworkPropertyMetadata(default(ParametersVM), (d, e) =>
        {
            if (d is ParametersControl self &&
                e.NewValue is ParametersVM value)
            {
                self.Gold = value.Gold;
            }
        }));

    public int Gold
    {
        get => (int)GetValue(GoldProperty);
        set => SetValue(GoldProperty, value);
    }

    // Using a DependencyProperty as the backing store for Gold.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GoldProperty = DependencyProperty.Register(
        "Gold", typeof(int), typeof(ParametersControl), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        {
            if (d is ParametersControl self &&
                e.NewValue is int value)
            {
                self.Source.Gold = value;
            }
        }));

    #endregion Dependency Property

    public ParametersControl()
    {
        InitializeComponent();
    }
}