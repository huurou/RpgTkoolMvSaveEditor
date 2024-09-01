using RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;
using RpgTkoolMvSaveEditor.Util.LogDisplays;
using System.Windows;

namespace RpgTkoolMvSaveEditor.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel, ILogDisplay logDisplay)
    {
        DataContext = viewModel;
        InitializeComponent();

        logDisplay.ShowLogRequested +=
            (s, e) => Application.Current?.Dispatcher.Invoke(
                () => ConsoleTextView.AppendConsoleItem(ConsoleTextItemProvider.Create(e.LogLevel, e.DateTime, e.Message))
            );
    }
}