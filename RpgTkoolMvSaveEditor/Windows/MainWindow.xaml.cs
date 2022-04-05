using RpgTkoolMvSaveEditor.Dialogs;
using System.Windows;

namespace RpgTkoolMvSaveEditor.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowVM vm_ = new();
    public MainWindow()
    {
        InitializeComponent();

        vm_.ErrorOccurred+= (s, message) => new ErrorDialog(message) { Owner = this}.ShowDialog();

        DataContext = vm_;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        vm_.OnLoaded();
    }
}