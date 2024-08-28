using Reactive.Bindings;
using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Presentation.Dialogs;

public class AboutDialogViewModel : ViewModelBase
{
    public ReactivePropertySlim<string> Title { get; } = new("Version Information");
    public ReactivePropertySlim<bool?> DialogResult { get; } = new();
    public ReactivePropertySlim<string> ProductName { get; } = new("");
    public ReactivePropertySlim<string> ProductVersion { get; } = new("");
    public ReactivePropertySlim<string> LegalCopyright { get; } = new("");
    public ReactivePropertySlim<string> Description { get; } = new("");

    public ReactiveCommand OkCmd { get; } = new();

    public AboutDialogViewModel()
    {
        var versionInfo = FileVersionInfo.GetVersionInfo(Environment.GetCommandLineArgs()[0]);
        // パッケージ::製品
        ProductName.Value = versionInfo.ProductName ?? "";
        // パッケージ::パッケージバージョン
        ProductVersion.Value = $"Version: {versionInfo.ProductVersion}";
        // パッケージ::著作権
        LegalCopyright.Value = versionInfo.LegalCopyright ?? "";
        // パッケージ::説明
        Description.Value = versionInfo.Comments ?? "";

        OkCmd.Subscribe(() => DialogResult.Value = true);
    }
}