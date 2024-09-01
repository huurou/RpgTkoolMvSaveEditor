using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Presentation.Dialogs;

public partial class AboutDialogViewModel : ObservableObject
{
    [ObservableProperty] private bool? dialogResult;
    [ObservableProperty] private string title = "Version Information";
    [ObservableProperty] private string? productName;
    [ObservableProperty] private string? productVersion;
    [ObservableProperty] private string? legalCopyright;
    [ObservableProperty] private string? description;

    [RelayCommand]
    public void Ok()
    {
        DialogResult = true;
    }

    [RelayCommand]
    public void Loaded()
    {
        var versionInfo = FileVersionInfo.GetVersionInfo(Environment.GetCommandLineArgs()[0]);
        // パッケージ::製品
        ProductName = versionInfo.ProductName;
        // パッケージ::パッケージバージョン
        ProductVersion = $"Version: {versionInfo.ProductVersion}";
        // パッケージ::著作権
        LegalCopyright = versionInfo.LegalCopyright ?? "<<LegalCopyright>>";
        // パッケージ::説明
        Description = versionInfo.Comments;
    }
}