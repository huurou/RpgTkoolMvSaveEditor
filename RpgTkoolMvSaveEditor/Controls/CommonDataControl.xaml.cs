using System.Windows.Controls;
using WinCopies.Linq;

namespace RpgTkoolMvSaveEditor.Controls;

/// <summary>
/// CommonDataEditorControl.xaml の相互作用ロジック
/// </summary>
public partial class CommonDataEditorControl : UserControl
{
    private readonly CommonDataControlVM vm_ = new();

    private IEnumerable<Switch> SelectedItems => DataGrid_Switches.SelectedItems.Select(x => (Switch)x);

    public CommonDataEditorControl()
    {
        InitializeComponent();

        DataContext = vm_;

        vm_.SetValueSelectedSwitches += (s, e) => e.action(SelectedItems, e.value);
    }
}