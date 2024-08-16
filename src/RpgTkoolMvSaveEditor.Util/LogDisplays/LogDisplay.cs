﻿using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using RpgTkoolMvSaveEditor.Util.Events;

namespace RpgTkoolMvSaveEditor.Util.LogDisplays;

public partial class LogDisplay : ILogDisplay
{
    public Event<ShowLogRequestedEventArgs> ShowLogRequested { get; } = new();

    public void ShowLog(LogLevel logLevel, DateTime dateTime, string message, params object?[]? args)
    {
        ShowLogRequested.Publish(new(logLevel, dateTime, string.Format(ToIndexFormat(message), args ?? [])));
    }

    /// <summary>
    /// {arg0}のような任意の文字列で構成されたFormatをstring.Formatに沿った{0}のようなIndexで構成されたFormatに変換する
    /// </summary>
    /// <param name="format">foramt</param>
    /// <returns></returns>
    private static string ToIndexFormat(string format)
    {
        var counter = 0;
        return IndexFormatRegex().Replace(format, _ => $"{{{counter++}}}");
    }

    [GeneratedRegex(@"\{.*?\}")]
    private static partial Regex IndexFormatRegex();
}