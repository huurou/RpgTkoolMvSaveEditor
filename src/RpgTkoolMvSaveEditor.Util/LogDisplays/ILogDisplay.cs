using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Util.Events;

namespace RpgTkoolMvSaveEditor.Util.LogDisplays;

/// <summary>
/// ログを画面に表示するインターフェース
/// </summary>
public interface ILogDisplay
{
    Event<ShowLogRequestedEventArgs> ShowLogRequested { get; }

    void ShowLog(LogLevel logLevel, DateTime dateTime, string message, params object?[]? args);
}

public record ShowLogRequestedEventArgs(LogLevel LogLevel, DateTime DateTime, string Message);