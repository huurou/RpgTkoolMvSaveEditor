using System.Windows;
using RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;
using RpgTkoolMvSaveEditor.Util.LogDisplays;

namespace RpgTkoolMvSaveEditor.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel, ILogDisplay logDisplay)
    {
        InitializeComponent();
        DataContext = viewModel;

        logDisplay.ShowLogRequested.Subscribe(
            e => Application.Current.Dispatcher.Invoke(
                () => ConsoleTextView.AppendConsoleItem(ConsoleTextItemProvider.Create(e.LogLevel, e.DateTime, e.Message))
            )
        );
    }
}