using Microsoft.Extensions.Logging;

namespace RpgTkoolMvSaveEditor.Util.LogDisplays;

public static class LogDisplayExtensions
{
    public static void LogTrace(this ILogDisplay logDisplay, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Trace, DateTime.Now, message, args);
    }

    public static void LogDebug(this ILogDisplay logDisplay, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Debug, DateTime.Now, message, args);
    }

    public static void LogInformation(this ILogDisplay logDisplay, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Information, DateTime.Now, message, args);
    }

    public static void LogWarning(this ILogDisplay logDisplay, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Warning, DateTime.Now, message, args);
    }

    public static void LogError(this ILogDisplay logDisplay, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Error, DateTime.Now, message, args);
    }

    public static void LogError(this ILogDisplay logDisplay, Exception ex, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Error, DateTime.Now, $"{message}{(string.IsNullOrEmpty(message) ? "" : Environment.NewLine)}{ex}", args);
    }

    public static void LogCritical(this ILogDisplay logDisplay, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Critical, DateTime.Now, message, args);
    }

    public static void LogCritical(this ILogDisplay logDisplay, Exception ex, string message, params object?[]? args)
    {
        logDisplay.Log(LogLevel.Critical, DateTime.Now, $"{message}{(string.IsNullOrEmpty(message) ? "" : Environment.NewLine)}{ex}", args);
    }
}