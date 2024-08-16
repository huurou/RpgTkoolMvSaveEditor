using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;

public class ConsoleTextItemPart : ConsoleTextItemBase
{
    private readonly Run run_;

    public Inline Inline => run_;

    public string Text { get => run_.Text; set => run_.Text = value; }

    public ConsoleTextItemPart()
    {
        run_ = new Run();
    }

    public ConsoleTextItemPart(ConsoleTextItemStyle itemStyle)
    {
        run_ = new Run();
        ItemStyle = itemStyle;
    }

    public ConsoleTextItemPart(string text)
    {
        run_ = new Run(text);
    }

    public ConsoleTextItemPart(ConsoleTextItemStyle itemStyle, string text)
    {
        run_ = new Run(text);
        ItemStyle = itemStyle;
    }

    protected override void ApplyBackground(Brush background)
    {
        base.ApplyBackground(background);
        run_.Background = background;
    }

    protected override void ApplyForeground(Brush foreground)
    {
        base.ApplyForeground(foreground);
        run_.Foreground = foreground;
    }

    protected override void ApplyFontFamily(FontFamily fontFamily)
    {
        base.ApplyFontFamily(fontFamily);
        run_.FontFamily = fontFamily;
    }

    protected override void ApplyFontSize(double fontSize)
    {
        base.ApplyFontSize(fontSize);
        run_.FontSize = fontSize;
    }

    protected override void ApplyFontStretch(FontStretch fontStretch)
    {
        base.ApplyFontStretch(fontStretch);
        run_.FontStretch = fontStretch;
    }

    protected override void ApplyFontStyle(FontStyle fontStyle)
    {
        base.ApplyFontStyle(fontStyle);
        run_.FontStyle = fontStyle;
    }

    protected override void ApplyFontWeight(FontWeight fontWeight)
    {
        base.ApplyFontWeight(fontWeight);
        run_.FontWeight = fontWeight;
    }

    protected override void ApplyTextDecorations(TextDecorationCollection textDecorations)
    {
        base.ApplyTextDecorations(textDecorations);
        run_.TextDecorations = textDecorations;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            ItemStyle = null;
        }
    }
}