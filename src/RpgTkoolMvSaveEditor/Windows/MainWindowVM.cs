using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Threading.Tasks;

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
    public DelegateCommand OpenDirCmd => openDirCmd_ ??= new DelegateCommand(async () => await OpenDirectoryAsync());

    private DelegateCommand? reloadCmd_;
    public DelegateCommand ReloadCmd => reloadCmd_ ??= new DelegateCommand(
        async () =>
        {
            if (Directory.Exists(dirPath_) && !await Dependency.App.LoadDirectoryAsync(dirPath_))
            {
                dirPath_ = null;
            }
        });

    #endregion Binding Command

    private string? dirPath_;

    public MainWindowVM()
    {
        Dependency.App.ErrorOccurred += (s, e) => ErrorOccurred?.Invoke(s, e);
        Dependency.App.DataLoaded += (s, e) => Title = $"{e}-{TITLE}";
    }

    public async Task OnLoadedAsync()
    {
        if (App.CommandArgs.Length == 0)
        {
            return;
        }

        dirPath_ = App.CommandArgs[0];
        if (!await Dependency.App.LoadDirectoryAsync(dirPath_))
        {
            dirPath_ = null;
        }
    }

    private async Task OpenDirectoryAsync()
    {
        using var dialog = new CommonOpenFileDialog
        {
            // フォルダ選択モードにする
            IsFolderPicker = true,
            // デフォルトではデスクトップを開くようにする
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Title = "ゲームフォルダかwwwフォルダを選択してください"
        };
        {
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            dirPath_ = dialog.FileName;
        }

        if (!await Dependency.App.LoadDirectoryAsync(dirPath_))
        {
            dirPath_ = null;
        }
    }
}