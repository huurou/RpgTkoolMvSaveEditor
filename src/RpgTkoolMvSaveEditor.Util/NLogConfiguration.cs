using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Util;

public static class NLogConfiguration
{
    public static void Configure(string logDirPath, ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        var layout = "${longdate} [${uppercase:${level:padding=-5}}] [${logger}] ${message}${onexception:${newline}${exception}}";
        var logFile = new FileTarget("logFile")
        {
            FileName = Path.Combine(logDirPath, "${shortdate}.log"),
            Layout = layout,
            ArchiveAboveSize = 100000000,
        };
        var logDebugger = new DebuggerTarget("logDebugger") { Layout = layout };
        var config = new LoggingConfiguration();
        SetLoggingConfiguration(config, logFile, logDebugger);
        loggingBuilder.AddNLog(config);
    }

    private static void SetLoggingConfiguration(LoggingConfiguration config, FileTarget logFile, DebuggerTarget logDebugger)
    {
        // DEBUG時のみ実行される
        OnDebug(config, logFile, logDebugger);
        // RELEASE時のみ実行される
        OnRelease(config, logFile, logDebugger);

        [Conditional("DEBUG")]
        static void OnDebug(LoggingConfiguration config, FileTarget logFile, DebuggerTarget logDebugger)
        {
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logFile);
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logDebugger);
        }

        [Conditional("RELEASE")]
        static void OnRelease(LoggingConfiguration config, FileTarget logFile, DebuggerTarget logDebugger)
        {
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logFile);
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logDebugger);
        }
    }
}