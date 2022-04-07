using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// SaveDataTabControl.xaml の相互作用ロジック
/// </summary>
public partial class SaveDataTabControl : UserControl
{
    private readonly SaveDataTabControlVM vm_ = new();

    public SaveDataTabControl()
    {
        InitializeComponent();

        DataContext = vm_;
    }
}