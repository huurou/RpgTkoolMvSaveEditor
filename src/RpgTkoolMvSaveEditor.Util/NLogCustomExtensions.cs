using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;

namespace RpgTkoolMvSaveEditor.Util;

public static class NLogCustomExtensions
{
    public static ILoggingBuilder AddCustomNLog(this ILoggingBuilder builder, string logDirPath)
    {
        var layout = "${longdate} [${uppercase:${level:padding=-5}}] [${logger}] ${message}${onexception:${newline}${exception}}";
        var logFile = new FileTarget("logFile")
        {
            FileName = Path.Combine(logDirPath, "${shortdate}.log"),
            Layout = layout,
            ArchiveAboveSize = 100000000,
        };
        var logDebugger = new DebuggerTarget("logDebugger") { Layout = layout };
        var config = new LoggingConfiguration();
        // 実際に出力するログレベルはappsettings.jsonで設定する
        config.AddRuleForAllLevels(logFile);
        config.AddRuleForAllLevels(logDebugger);
        builder.AddNLog(config);
        return builder;
    }
}
