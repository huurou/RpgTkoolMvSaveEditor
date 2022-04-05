using Microsoft.WindowsAPICodePack.Dialogs;
using RpgTkoolMvSaveEditor.Dialogs;
using System.Windows;

namespace RpgTkoolMvSaveEditor.Windows;

internal class MainWindowVM : NotifycationObject
{
    #region Binding Command

    private DelegateCommand? openDirCmd_;
    public DelegateCommand OpenDirCmd => openDirCmd_ ??= new DelegateCommand(OpenDirectory);

    #endregion Binding Command

    public MainWindowVM(Window window)
    {
        Dependency.App.ErrorOcuured += (s, message) => new ErrorDialog(message) { Owner = window }.ShowDialog();
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