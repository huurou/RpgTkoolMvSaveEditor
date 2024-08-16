using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;

public class ConsoleTextItemStyle : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private FontFamily? fontFamily_;
    private FontStyle? fontStyle_;
    private FontWeight? fontWeight_;
    private FontStretch? fontStretch_;
    private double? fontSize_;
    private Brush? foreground_;
    private Brush? background_;
    private TextDecorationCollection? textDecorations_;

    public FontFamily? FontFamily
    {
        get => fontFamily_;
        set
        {
            var prevValue = fontFamily_;
            if (prevValue == value)
            {
                return;
            }

            fontFamily_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontFamily)));
            if (prevValue != FontFamilyInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontFamilyInheritance)));
            }
        }
    }
    public FontStyle? FontStyle
    {
        get => fontStyle_;
        set
        {
            var prevValue = fontStyle_;
            if (prevValue == value)
            {
                return;
            }

            fontStyle_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStyle)));
            if (prevValue != FontStyleInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStyleInheritance)));
            }
        }
    }
    public FontWeight? FontWeight
    {
        get => fontWeight_;
        set
        {
            var prevValue = fontWeight_;
            if (prevValue == value)
            {
                return;
            }

            fontWeight_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontWeight)));
            if (prevValue != FontWeightInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontWeightInheritance)));
            }
        }
    }
    public FontStretch? FontStretch
    {
        get => fontStretch_;
        set
        {
            var prevValue = fontStretch_;
            if (prevValue == value)
            {
                return;
            }

            fontStretch_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStretch)));
            if (prevValue != FontStretchInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStretchInheritance)));
            }
        }
    }
    public double? FontSize
    {
        get => fontSize_;
        set
        {
            var prevValue = fontSize_;
            if (prevValue == value)
            {
                return;
            }

            fontSize_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSize)));
            if (prevValue != FontSizeInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSizeInheritance)));
            }
        }
    }
    public Brush? Foreground
    {
        get => foreground_;
        set
        {
            var prevValue = foreground_;
            if (prevValue == value)
            {
                return;
            }

            foreground_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Foreground)));
            if (prevValue != ForegroundInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForegroundInheritance)));
            }
        }
    }
    public Brush? Background
    {
        get => background_;
        set
        {
            var prevValue = background_;
            if (prevValue == value)
            {
                return;
            }

            background_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Background)));
            if (prevValue != BackgroundInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundInheritance)));
            }
        }
    }
    public TextDecorationCollection? TextDecorations
    {
        get => textDecorations_;
        set
        {
            var prevValue = textDecorations_;
            if (prevValue == value)
            {
                return;
            }

            textDecorations_ = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextDecorations)));
            if (prevValue != TextDecorationsInheritance)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextDecorationsInheritance)));
            }
        }
    }
    public FontFamily? FontFamilyInheritance => fontFamily_ ?? baseStyle_?.FontFamilyInheritance;
    public FontStyle? FontStyleInheritance => fontStyle_ ?? baseStyle_?.FontStyleInheritance;
    public FontWeight? FontWeightInheritance => fontWeight_ ?? baseStyle_?.FontWeightInheritance;
    public FontStretch? FontStretchInheritance => fontStretch_ ?? baseStyle_?.FontStretchInheritance;
    public double? FontSizeInheritance => fontSize_ ?? baseStyle_?.FontSizeInheritance;
    public Brush? ForegroundInheritance => foreground_ ?? baseStyle_?.ForegroundInheritance;
    public Brush? BackgroundInheritance => background_ ?? baseStyle_?.BackgroundInheritance;
    public TextDecorationCollection? TextDecorationsInheritance => textDecorations_ ?? baseStyle_?.TextDecorationsInheritance;

    private readonly ConsoleTextItemStyle? baseStyle_;

    public ConsoleTextItemStyle()
    {
        fontFamily_ = SystemFonts.MessageFontFamily;
        foreground_ = Brushes.Black;
    }

    public ConsoleTextItemStyle(ConsoleTextItemStyle baseStyle)
    {
        baseStyle_ = baseStyle;
        if (baseStyle != null)
        {
            baseStyle.PropertyChanged += BaseStyle_PropertyChanged;
        }
    }

    private void BaseStyle_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(FontFamilyInheritance):
                if (FontFamily is null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontFamilyInheritance)));
                }

                break;

            case nameof(FontStyleInheritance):
                if (!FontStyle.HasValue)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStyleInheritance)));
                }

                break;

            case nameof(FontWeightInheritance):
                if (!FontWeight.HasValue)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontWeightInheritance)));
                }

                break;

            case nameof(FontStretchInheritance):
                if (!FontStretch.HasValue)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStretchInheritance)));
                }

                break;

            case nameof(FontSizeInheritance):
                if (!FontSize.HasValue)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSizeInheritance)));
                }

                break;

            case nameof(ForegroundInheritance):
                if (Foreground is null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForegroundInheritance)));
                }

                break;

            case nameof(BackgroundInheritance):
                if (Background is null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundInheritance)));
                }

                break;

            case nameof(TextDecorationsInheritance):
                if (TextDecorations is null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextDecorationsInheritance)));
                }

                break;

            default:
                break;
        }
    }
}