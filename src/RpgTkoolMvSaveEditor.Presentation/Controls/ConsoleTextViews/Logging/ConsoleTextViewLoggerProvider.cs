namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.Logging;

[ProviderAlias("ConsoleTextViewLogger")]
public class ConsoleTextViewLoggerProvider : ILoggerProvider
{
    private readonly ILogger logger_ = new ConsoleTextViewLogger();

    public ILogger CreateLogger(string categoryName)
    {
        return logger_;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
