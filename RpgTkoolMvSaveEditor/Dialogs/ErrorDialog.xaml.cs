using System.Windows;

namespace RpgTkoolMvSaveEditor.Dialogs;

/// <summary>
/// ErrorDialog.xaml の相互作用ロジック
/// </summary>
public partial class ErrorDialog : Window
{
    public ErrorDialog(string message)
    {
        InitializeComponent();

        TextBlock_Message.Text = message;
    }
}