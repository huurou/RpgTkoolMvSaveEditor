using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RpgTkoolMvSaveEditor.Util.LogDisplays;

public class LogDisplayLogger(ILogDisplay logDisplay) : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new Scope<TState>(state);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        var enabled = false;
        OnDebug(logLevel, ref enabled);
        OnRelease(logLevel, ref enabled);
        return enabled;

        [Conditional("DEBUG")]
        static void OnDebug(LogLevel logLevel, ref bool enabled)
        {
            enabled = logLevel switch
            {
                LogLevel.Trace or LogLevel.Debug or LogLevel.Information or LogLevel.Warning or LogLevel.Error or LogLevel.Critical => true,
                LogLevel.None or _ => false,
            };
        }

        [Conditional("RELEASE")]
        static void OnRelease(LogLevel logLevel, ref bool enabled)
        {
            enabled = logLevel switch
            {
                LogLevel.Information or LogLevel.Warning or LogLevel.Error or LogLevel.Critical => true,
                LogLevel.Trace or LogLevel.Debug or LogLevel.None or _ => false,
            };
        }
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) { return; }
        logDisplay.Log(logLevel, DateTime.Now, formatter(state, exception));
    }

    /// <summary>
    /// スコープ
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    private class Scope<TState> : IDisposable
    {
        internal Scope(TState state)
        {
            State = state;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public TState State { get; }
    }
}
