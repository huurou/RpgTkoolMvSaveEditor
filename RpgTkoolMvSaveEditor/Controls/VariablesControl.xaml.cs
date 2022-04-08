using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// VariablesControl.xaml の相互作用ロジック
/// </summary>
public partial class VariablesControl : UserControl
{
    #region Dependency Property

    internal ObservableCollection<VariableVM> Source
    {
        get => (ObservableCollection<VariableVM>)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        "Source", typeof(ObservableCollection<VariableVM>), typeof(VariablesControl), new FrameworkPropertyMetadata(default(ObservableCollection<VariableVM>)));

    #endregion Dependency Property

    public VariablesControl()
    {
        InitializeComponent();
    }
}