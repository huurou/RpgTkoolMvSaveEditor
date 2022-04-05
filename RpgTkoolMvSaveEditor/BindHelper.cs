using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NameSpace;

internal abstract class NotifycationObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void SetProperty<T>(ref T target, T value, [CallerMemberName] string caller = "")
    {
        target = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}

internal class DelegateCommand : ICommand
{
    private readonly Action execute_;
    private readonly Func<bool>? canExecute_;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    internal DelegateCommand(Action execute, Func<bool>? canExecute = null)
    {
        execute_ = execute;
        canExecute_ = canExecute;
    }

    public void Execute(object? parameter) => execute_?.Invoke();

    public bool CanExecute(object? parameter) => canExecute_?.Invoke() ?? true;
}

internal class DelegateCommand<T> : ICommand
{
    private readonly Action<T> execute_;
    private readonly Func<T, bool>? canExecute_;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    internal DelegateCommand(Action<T> execute, Func<T, bool>? canExecute = null)
    {
        execute_ = execute;
        canExecute_ = canExecute;
    }

    public void Execute(object? parameter) => execute_?.Invoke(parameter is T param ? param : throw new ArgumentException(null, nameof(parameter)));

    public bool CanExecute(object? parameter) => canExecute_?.Invoke(parameter is T param ? param : throw new ArgumentException(null, nameof(parameter))) ?? true;
}