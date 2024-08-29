using System.Windows;

namespace RpgTkoolMvSaveEditor.Presentation.Dialogs;

/// <summary>
/// AboutDialog.xaml の相互作用ロジック
/// </summary>
public partial class AboutDialog : Window
{
    public AboutDialog(AboutDialogViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
