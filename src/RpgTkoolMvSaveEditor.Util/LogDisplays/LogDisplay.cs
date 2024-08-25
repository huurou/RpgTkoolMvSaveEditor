using Microsoft.Extensions.Logging;
using RpgTkoolMvSaveEditor.Util.Events;
using System.Text.RegularExpressions;

namespace RpgTkoolMvSaveEditor.Util.LogDisplays;

public class LogDisplay : ILogDisplay
{
    public Event<ShowLogRequestedEventArgs> ShowLogRequested { get; } = new();
#pragma warning disable SYSLIB1045 // 'GeneratedRegexAttribute' に変換します。
    private readonly Regex indexFormatRegex_ = new(@"\{.*?\}");
#pragma warning restore SYSLIB1045 // 'GeneratedRegexAttribute' に変換します。

    public void ShowLog(LogLevel logLevel, DateTime dateTime, string message, params object?[]? args)
    {
        ShowLogRequested.Publish(new(logLevel, dateTime, string.Format(ToIndexFormat(message), args ?? [])));
    }

    /// <summary>
    /// {arg0}のような任意の文字列で構成されたFormatをstring.Formatに沿った{0}のようなIndexで構成されたFormatに変換する
    /// </summary>
    /// <param name="format">foramt</param>
    /// <returns></returns>
    private string ToIndexFormat(string format)
    {
        var counter = 0;
        return indexFormatRegex_.Replace(format, _ => $"{{{counter++}}}");
    }
}
