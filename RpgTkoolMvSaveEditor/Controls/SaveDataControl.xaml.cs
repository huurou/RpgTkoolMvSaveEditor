using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// SaveDataControl.xaml の相互作用ロジック
/// </summary>
public partial class SaveDataControl : UserControl
{
    private readonly SaveDataControlVM vm_ = new();

    public SaveDataControl()
    {
        InitializeComponent();

        DataContext = vm_;
    }
}