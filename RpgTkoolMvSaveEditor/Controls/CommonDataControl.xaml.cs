using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// CommonDataEditorControl.xaml の相互作用ロジック
/// </summary>
public partial class CommonDataEditorControl : UserControl
{
    private readonly CommonDataControlVM vm_ = new();

    public CommonDataEditorControl()
    {
        InitializeComponent();

        DataContext = vm_;
    }
}