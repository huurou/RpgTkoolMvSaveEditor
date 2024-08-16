using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;

public abstract class ConsoleTextItemBase : IDisposable
{
    private ConsoleTextItemStyle? itemStyle_;
    private FontFamily baseFontFamily_;
    private FontStyle baseFontStyle_;
    private FontWeight baseFontWeight_;
    private FontStretch baseFontStretch_;
    private double baseFontSize_;
    private Brush baseForeground_;

    public ConsoleTextItemStyle? ItemStyle
    {
        get => itemStyle_;
        set
        {
            var prev = itemStyle_;
            itemStyle_ = value;
            if (prev != value)
            {
                if (prev != null)
                {
                    prev.PropertyChanged -= ItemStyle_PropertyChanged;
                }

                if (value != null)
                {
                    value.PropertyChanged += ItemStyle_PropertyChanged;
                }

                ApplyFontFamily();
                ApplyFontStyle();
                ApplyFontWeight();
                ApplyFontStretch();
                ApplyFontSize();
                ApplyForeground();
                ApplyBackground();
                ApplyTextDecorations();
            }
        }
    }
    public FontFamily BaseFontFamily
    {
        get => baseFontFamily_;
        set
        {
            if (baseFontFamily_ == value)
            {
                return;
            }

            baseFontFamily_ = value;
            ApplyFontFamily();
        }
    }
    public FontStyle BaseFontStyle
    {
        get => baseFontStyle_;
        set
        {
            if (baseFontStyle_ == value)
            {
                return;
            }

            baseFontStyle_ = value;
            ApplyFontStyle();
        }
    }
    public FontWeight BaseFontWeight
    {
        get => baseFontWeight_;
        set
        {
            if (baseFontWeight_ == value)
            {
                return;
            }

            baseFontWeight_ = value;
            ApplyFontWeight();
        }
    }
    public FontStretch BaseFontStretch
    {
        get => baseFontStretch_;
        set
        {
            if (baseFontStretch_ == value)
            {
                return;
            }

            baseFontStretch_ = value;
            ApplyFontStretch();
        }
    }
    public double BaseFontSize
    {
        get => baseFontSize_;
        set
        {
            if (baseFontSize_ == value)
            {
                return;
            }

            baseFontSize_ = value;
            ApplyFontSize();
        }
    }
    public Brush BaseForeground
    {
        get => baseForeground_;
        set
        {
            if (baseForeground_ == value)
            {
                return;
            }

            baseForeground_ = value;
            ApplyForeground();
        }
    }

    private ConsoleTextView? parentControl_;
    private bool disposed_;

    public ConsoleTextItemBase()
    {
        baseFontFamily_ = SystemFonts.MessageFontFamily;
        baseFontStyle_ = SystemFonts.MessageFontStyle;
        baseFontWeight_ = SystemFonts.MessageFontWeight;
        baseFontStretch_ = FontStretches.Normal;
        baseFontSize_ = SystemFonts.MessageFontSize;
        baseForeground_ = Brushes.Black;
    }

    ~ConsoleTextItemBase()
    {
        Dispose(false);
    }

    internal void BindControlProperties(ConsoleTextView? control)
    {
        if (parentControl_ == control)
        {
            return;
        }

        if (parentControl_ != null)
        {
            parentControl_.PropertyChanged -= Control_PropertyChanged;
        }

        parentControl_ = control;
        if (control is null)
        {
            return;
        }

        BaseFontFamily = control.FontFamily;
        BaseFontStyle = control.FontStyle;
        BaseFontWeight = control.FontWeight;
        BaseFontSize = control.FontSize;
        BaseForeground = control.Foreground;

        control.PropertyChanged += Control_PropertyChanged;
    }

    private void Control_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ConsoleTextView.FontFamily): ApplyFontFamily(); break;
            case nameof(ConsoleTextView.FontStyle): ApplyFontStyle(); break;
            case nameof(ConsoleTextView.FontWeight): ApplyFontWeight(); break;
            case nameof(ConsoleTextView.FontStretch): ApplyFontStretch(); break;
            case nameof(ConsoleTextView.FontSize): ApplyFontSize(); break;
            case nameof(ConsoleTextView.Foreground): ApplyForeground(); break;
            case nameof(ConsoleTextView.Background): ApplyBackground(); break;
            case nameof(ConsoleTextView.TextDecorations): ApplyTextDecorations(); break;
            default: break;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed_)
        {
            if (disposing)
            {
                ItemStyle = null;
                BindControlProperties(null);
            }
            disposed_ = true;
        }
    }

    protected virtual void ApplyFontFamily(FontFamily fontFamily)
    { }

    protected virtual void ApplyFontStyle(FontStyle fontStyle)
    { }

    protected virtual void ApplyFontWeight(FontWeight fontWeight)
    { }

    protected virtual void ApplyFontStretch(FontStretch fontStretch)
    { }

    protected virtual void ApplyFontSize(double fontSize)
    { }

    protected virtual void ApplyForeground(Brush foreground)
    { }

    protected virtual void ApplyBackground(Brush background)
    { }

    protected virtual void ApplyTextDecorations(TextDecorationCollection textDecorations)
    { }

    private void ApplyFontFamily()
    {
        ApplyFontFamily(itemStyle_?.FontFamily ?? BaseFontFamily);
    }

    private void ApplyFontStyle()
    {
        ApplyFontStyle(itemStyle_?.FontStyleInheritance ?? BaseFontStyle);
    }

    private void ApplyFontWeight()
    {
        ApplyFontWeight(itemStyle_?.FontWeightInheritance ?? BaseFontWeight);
    }

    private void ApplyFontStretch()
    {
        ApplyFontStretch(itemStyle_?.FontStretchInheritance ?? BaseFontStretch);
    }

    private void ApplyFontSize()
    {
        ApplyFontSize(itemStyle_?.FontSizeInheritance ?? BaseFontSize);
    }

    private void ApplyForeground()
    {
        ApplyForeground(itemStyle_?.ForegroundInheritance ?? BaseForeground);
    }

    private void ApplyBackground()
    {
        ApplyBackground(itemStyle_?.BackgroundInheritance!);
    }

    private void ApplyTextDecorations()
    {
        ApplyTextDecorations(itemStyle_?.TextDecorationsInheritance!);
    }

    private void ItemStyle_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ConsoleTextItemStyle.FontFamilyInheritance):
                ApplyFontFamily();
                break;

            case nameof(ConsoleTextItemStyle.FontStyleInheritance):
                ApplyFontStyle();
                break;

            case nameof(ConsoleTextItemStyle.FontWeightInheritance):
                ApplyFontWeight();
                break;

            case nameof(ConsoleTextItemStyle.FontStretchInheritance):
                ApplyFontStretch();
                break;

            case nameof(ConsoleTextItemStyle.FontSizeInheritance):
                ApplyFontSize();
                break;

            case nameof(ConsoleTextItemStyle.ForegroundInheritance):
                ApplyForeground();
                break;

            case nameof(ConsoleTextItemStyle.BackgroundInheritance):
                ApplyBackground();
                break;

            case nameof(ConsoleTextItemStyle.TextDecorationsInheritance):
                ApplyTextDecorations();
                break;

            default:
                break;
        }
    }
}