using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace RpgTkoolMvSaveEditor.Windows;

internal class MainWindowVM : NotificationObject
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

    private DelegateCommand? reloadCmd_;
    public DelegateCommand ReloadCmd => reloadCmd_ ??= new DelegateCommand(
        () =>
        {
            if (Directory.Exists(dirPath_) && !Dependency.App.LoadDirectory(dirPath_)) dirPath_ = null;
        });

    #endregion Binding Command

    private string? dirPath_;

    public MainWindowVM()
    {
        Dependency.App.ErrorOccurred += (s, e) => ErrorOccurred?.Invoke(s, e);
        Dependency.App.DataLoaded += (s, e) => Title = $"{e}-{TITLE}";
    }

    public void OnLoaded()
    {
        if (App.CommandArgs.Length == 0) return;
        dirPath_ = App.CommandArgs[0];
        if (!Dependency.App.LoadDirectory(dirPath_)) dirPath_ = null;
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
        dirPath_ = dialog.FileName;
        if (!Dependency.App.LoadDirectory(dirPath_)) dirPath_ = null;
    }
}