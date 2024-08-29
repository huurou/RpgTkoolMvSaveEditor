using System.ComponentModel;
using System.Windows;

namespace RpgTkoolMvSaveEditor.Presentation.Dialogs.Common;

public class DialogService<TDialog>(IServiceProvider provider) where TDialog : Window
{
    public bool? ShowDialog()
    {
        var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
        var dialog = provider.GetRequiredService<TDialog>();
        if (activeWindow is not null)
        {
            dialog.Owner = activeWindow;
        }
        return dialog.ShowDialog();
    }
}

public class DialogService<TDialog, TViewModel>(IServiceProvider provider) where TDialog : Window where TViewModel : INotifyPropertyChanged
{
    public bool? ShowDialog(Action<TViewModel>? onOpened = null, Action<TViewModel, bool?>? onClosed = null)
    {
        var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
        var dialog = provider.GetRequiredService<TDialog>();
        if (activeWindow is not null)
        {
            dialog.Owner = activeWindow;
        }
        var viewModel = (TViewModel)dialog.DataContext;
        onOpened?.Invoke(viewModel);
        var result = dialog.ShowDialog();
        onClosed?.Invoke(viewModel, result);
        return result;
    }
}