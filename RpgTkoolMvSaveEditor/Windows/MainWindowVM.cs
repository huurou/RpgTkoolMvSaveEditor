using Microsoft.WindowsAPICodePack.Dialogs;

namespace RpgTkoolMvSaveEditor.Windows;

internal class MainWindowVM : NotifycationObject
{
    public event EventHandler<string>? ErrorOccurred;

    private const string TITLE = "RPGツクールMVセーブエディタ";

    #region Binding Property

    private string title_ = TITLE;
    public string Title { get => title_; set => SetProperty(ref title_, value); }

    #endregion Binding Property

    #region Binding Command

    private DelegateCommand? openDirCmd_;
    public DelegateCommand OpenDirCmd => openDirCmd_ ??= new DelegateCommand(OpenDirectory);

    #endregion Binding Command

    public MainWindowVM()
    {
        Dependency.App.ErrorOccurred += (s, e) => ErrorOccurred?.Invoke(s, e);
        Dependency.App.DataLoaded += (s, e) => Title = $"{e}-{TITLE}";
    }

    public void OnLoaded()
    {
        if (App.CommandArgs.Length > 0)
        {
            Dependency.App.LoadDirectory(App.CommandArgs[0]);
        }
    }

    private void OpenDirectory()
    {
        var dialog = new CommonOpenFileDialog
        {
            // フォルダ選択モードにする
            IsFolderPicker = true,
            // デフォルトではデスクトップを開くようにする
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Title = "ゲームフォルダかwwwフォルダを選択してください"
        };
        if (dialog.ShowDialog() != CommonFileDialogResult.Ok) return;
        Dependency.App.LoadDirectory(dialog.FileName);
    }
}