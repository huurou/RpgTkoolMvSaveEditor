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
        ProductName.Value = versionInfo.ProductName ?? "";
        ProductVersion.Value = $"Version: {versionInfo.ProductVersion}";
        LegalCopyright.Value = versionInfo.LegalCopyright ?? "";
        Description.Value = versionInfo.Comments ?? "";

        OkCmd.Subscribe(() => DialogResult.Value = true);
    }
}