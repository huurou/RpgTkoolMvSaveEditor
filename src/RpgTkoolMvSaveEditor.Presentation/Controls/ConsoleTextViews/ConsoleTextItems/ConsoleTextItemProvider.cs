namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;

public static class ConsoleTextItemProvider
{
    public static ConsoleTextItem Create(LogLevel logLevel, DateTime eventTime, string consoleText)
    {
        return logLevel switch
        {
            LogLevel.Debug => Detail(eventTime, consoleText),
            LogLevel.Information => Info(eventTime, consoleText),
            LogLevel.Warning => Warn(eventTime, consoleText),
            LogLevel.Error => Error(eventTime, consoleText),
            LogLevel.Critical => Fatal(eventTime, consoleText),
            _ => Fatal(eventTime, consoleText),
        };
    }

    private static string Format(DateTime eventTime, string consoleText)
    {
        return $"{eventTime:yyyy-MM-dd HH:mm:ss.fff} {consoleText}";
    }

    public static ConsoleTextItem Fatal(DateTime eventTime, string consoleText)
    {
        return new ConsoleTextItem(ConsoleTextItemStyles.Fatal, Format(eventTime, consoleText));
    }

    public static ConsoleTextItem Error(DateTime eventTime, string consoleText)
    {
        return new ConsoleTextItem(ConsoleTextItemStyles.Error, Format(eventTime, consoleText));
    }

    public static ConsoleTextItem Warn(DateTime eventTime, string consoleText)
    {
        return new ConsoleTextItem(ConsoleTextItemStyles.Warn, Format(eventTime, consoleText));
    }

    public static ConsoleTextItem Info(DateTime eventTime, string consoleText)
    {
        return new ConsoleTextItem(ConsoleTextItemStyles.Info, Format(eventTime, consoleText));
    }

    public static ConsoleTextItem Detail(DateTime eventTime, string consoleText)
    {
        return new ConsoleTextItem(ConsoleTextItemStyles.Detail, Format(eventTime, consoleText));
    }
}