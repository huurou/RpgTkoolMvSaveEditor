namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.Logging;

public static class ConsoleTextViewLoggerExtensions
{
    public static ILoggingBuilder AddConsoleTextView(this ILoggingBuilder builder)
    {
        builder.Services.AddSingleton<ILoggerProvider, ConsoleTextViewLoggerProvider>();
        return builder;
    }
}
