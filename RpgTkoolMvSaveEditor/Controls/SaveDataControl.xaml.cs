using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// SaveDataControl.xaml の相互作用ロジック
/// </summary>
public partial class SaveDataControl : UserControl
{
    #region Dependency Property

    internal SaveDataControlVM Source
    {
        get => (SaveDataControlVM)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(SaveDataControlVM), typeof(SaveDataControl), new FrameworkPropertyMetadata(default(SaveDataControlVM), (d, e) =>
        {
            if (d is SaveDataControl self &&
                e.NewValue is SaveDataControlVM value)
            {
                self.DataContext = value;
            }
        }));

    #endregion Dependency Property

    public SaveDataControl()
    {
        InitializeComponent();
    }
}