using Microsoft.Extensions.Logging;

namespace RpgTkoolMvSaveEditor.Util.LogDisplays;

[ProviderAlias("LogDisplayLogger")]
public class LogDisplayLoggerProvider(ILogDisplay logDisplay) : ILoggerProvider
{
    /// <summary>
    /// ロガーを生成します。
    /// </summary>
    /// <param name="categoryName">カテゴリー</param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        return new LogDisplayLogger(logDisplay);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
