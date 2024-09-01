namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.Logging;

public class ConsoleTextViewLogger : ILogger
{
    public static event EventHandler<LoggingRequestedEventArgs>? LoggingRequested;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new Scope<TState>(state);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) { return; }
        var message = formatter(state, exception);
        if (exception is not null)
        {
            message = $"{(string.IsNullOrEmpty(message) ? "" : $"{message}{Environment.NewLine}")}{exception}";
        }
        LoggingRequested?.Invoke(this, new(logLevel, DateTime.Now, message));
    }

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

public record LoggingRequestedEventArgs(LogLevel LogLevel, DateTime DateTime, string Message);
