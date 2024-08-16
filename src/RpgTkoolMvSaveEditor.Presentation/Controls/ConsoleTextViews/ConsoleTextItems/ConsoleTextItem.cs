using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;

public class ConsoleTextItem : ConsoleTextItemBase
{
    private readonly ConsoleTextItemPart?[] itemParts_;

    public ReadOnlyCollection<ConsoleTextItemPart?> Parts => Array.AsReadOnly(itemParts_);

    public Paragraph Paragraph { get; }

    public ConsoleTextItem(string text)
    {
        using var part = new ConsoleTextItemPart(text);
        itemParts_ = [part];
        Paragraph = new Paragraph
        {
            Margin = new Thickness(0),
            Padding = new Thickness(0),
            TextAlignment = TextAlignment.Left
        };
        Paragraph.Inlines.Add(part.Inline);
    }

    public ConsoleTextItem(ConsoleTextItemStyle itemStyle, string text)
    {
        using var part = new ConsoleTextItemPart(text);
        itemParts_ = [part];
        Paragraph = new Paragraph
        {
            Margin = new Thickness(0),
            Padding = new Thickness(0),
            TextAlignment = TextAlignment.Left
        };
        Paragraph.Inlines.Add(part.Inline);
        ItemStyle = itemStyle;
    }

    public ConsoleTextItem(params ConsoleTextItemPart[] parts)
    {
        itemParts_ = new ConsoleTextItemPart[parts.Length];
        Paragraph = new Paragraph();
        for (var i = 0; i < parts.Length; i++)
        {
            itemParts_[i] = parts[i];
            Paragraph.Inlines.Add(parts[i].Inline);
        }
        Paragraph.Inlines.Add(new LineBreak());
    }

    public ConsoleTextItem(ConsoleTextItemStyle itemStyle, params ConsoleTextItemPart[] parts)
    {
        itemParts_ = new ConsoleTextItemPart[parts.Length];
        Paragraph = new Paragraph();
        for (var i = 0; i < parts.Length; i++)
        {
            itemParts_[i] = parts[i];
            Paragraph.Inlines.Add(parts[i].Inline);
        }
        Paragraph.Inlines.Add(new LineBreak());

        ItemStyle = itemStyle;
    }

    protected override void ApplyBackground(Brush background)
    {
        base.ApplyBackground(background);
        Paragraph.Background = background;
    }

    protected override void ApplyForeground(Brush foreground)
    {
        base.ApplyForeground(foreground);
        Paragraph.Foreground = foreground;
    }

    protected override void ApplyFontFamily(FontFamily fontFamily)
    {
        base.ApplyFontFamily(fontFamily);
        Paragraph.FontFamily = fontFamily;
    }

    protected override void ApplyFontSize(double fontSize)
    {
        base.ApplyFontSize(fontSize);
        Paragraph.FontSize = fontSize;
    }

    protected override void ApplyFontStretch(FontStretch fontStretch)
    {
        base.ApplyFontStretch(fontStretch);
        Paragraph.FontStretch = fontStretch;
    }

    protected override void ApplyFontStyle(FontStyle fontStyle)
    {
        base.ApplyFontStyle(fontStyle);
        Paragraph.FontStyle = fontStyle;
    }

    protected override void ApplyFontWeight(FontWeight fontWeight)
    {
        base.ApplyFontWeight(fontWeight);
        Paragraph.FontWeight = fontWeight;
    }

    protected override void ApplyTextDecorations(TextDecorationCollection textDecorations)
    {
        base.ApplyTextDecorations(textDecorations);
        Paragraph.TextDecorations = textDecorations;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing)
        {
            return;
        }

        for (var i = 0; i < itemParts_.Length; i++)
        {
            itemParts_[i]?.Dispose();
            itemParts_[i] = null;
        }
        Paragraph.Inlines.Clear();
    }
}