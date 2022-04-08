using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// CommonDataEditorControl.xaml の相互作用ロジック
/// </summary>
public partial class CommonDataControl : UserControl
{
    private readonly CommonDataControlVM vm_ = new();

    public CommonDataControl()
    {
        InitializeComponent();

        DataContext = vm_;
    }
}