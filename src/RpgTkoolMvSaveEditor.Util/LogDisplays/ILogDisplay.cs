using Microsoft.Extensions.Logging;

namespace RpgTkoolMvSaveEditor.Util.LogDisplays;

/// <summary>
/// ログを画面に表示するインターフェース
/// </summary>
public interface ILogDisplay
{
    event EventHandler<ShowLogRequestedEventArgs>? ShowLogRequested;

    void ShowLog(LogLevel logLevel, DateTime dateTime, string message, params object?[]? args);
}

public record ShowLogRequestedEventArgs(LogLevel LogLevel, DateTime DateTime, string Message);