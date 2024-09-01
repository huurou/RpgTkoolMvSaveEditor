using RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews;
using RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;
using RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.Logging;
using System.Windows;

namespace RpgTkoolMvSaveEditor.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();

        ConsoleTextViewLogger.LoggingRequested +=
            (s, e) => Application.Current?.Dispatcher.Invoke(
                () => ConsoleTextView.AppendConsoleItem(ConsoleTextItemProvider.Create(e.LogLevel, e.DateTime, e.Message))
            );
    }
}