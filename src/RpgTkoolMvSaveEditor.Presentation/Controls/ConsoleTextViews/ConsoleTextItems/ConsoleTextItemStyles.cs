using System.Windows.Media;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;

public static class ConsoleTextItemStyles
{
    public static ConsoleTextItemStyle Fatal { get; } = new ConsoleTextItemStyle { Foreground = ToFreezedBrush(Colors.Red) };
    public static ConsoleTextItemStyle Error { get; } = new ConsoleTextItemStyle { Foreground = ToFreezedBrush(Colors.Orange) };
    public static ConsoleTextItemStyle Warn { get; } = new ConsoleTextItemStyle { Foreground = ToFreezedBrush(Colors.Yellow) };
    public static ConsoleTextItemStyle Info { get; } = new ConsoleTextItemStyle { Foreground = ToFreezedBrush(Color.FromRgb(192, 192, 255)) };
    public static ConsoleTextItemStyle Detail { get; } = new ConsoleTextItemStyle { Foreground = ToFreezedBrush(Color.FromRgb(192, 192, 192)) };

    private static SolidColorBrush ToFreezedBrush(Color color)
    {
        var brush = new SolidColorBrush(color);
        brush.Freeze();
        return brush;
    }
}