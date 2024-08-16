using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews;

public class SnappingScrollViewer : FlowDocumentScrollViewer
{
    private ScrollViewer? scrollViewer_;

    public SnappingTypes Snapping
    {
        get => (SnappingTypes)GetValue(SnappingProperty);
        set => SetValue(SnappingProperty, value);
    }

    public static readonly DependencyProperty SnappingProperty = DependencyProperty.Register(
        nameof(Snapping), typeof(SnappingTypes), typeof(SnappingScrollViewer), new FrameworkPropertyMetadata(SnappingTypes.None));

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        FindScrollViewer();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.Property.Name)
        {
            case nameof(HorizontalScrollBarVisibility):
            case nameof(VerticalScrollBarVisibility):
                FindScrollViewer();
                break;

            default:
                break;
        }
    }

    private void FindScrollViewer()
    {
        if (scrollViewer_ != null) { return; }
        ApplyTemplate();
        if ((scrollViewer_ = Template?.FindName("PART_ContentHost", this) as ScrollViewer) != null)
        {
            scrollViewer_.ScrollChanged += ScrollViewer_ScrollChanged;
        }
    }

    private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if ((Snapping & SnappingTypes.Bottom) != 0 &&
            (e.ExtentHeightChange > 0 || e.ViewportHeightChange < 0) &&
            e.VerticalOffset + e.ViewportHeight < e.ExtentHeight - 1)
        {
            var prevScrollBottom = e.VerticalOffset - e.VerticalChange + e.ViewportHeight - e.ViewportHeightChange;
            var prevExtentHeight = e.ExtentHeight - e.ExtentHeightChange;
            if (prevScrollBottom > prevExtentHeight - 1)
            {
                scrollViewer_?.ScrollToVerticalOffset(e.ExtentHeight - e.ViewportHeight);
            }
        }
        if ((Snapping & SnappingTypes.Right) != 0 &&
            (e.ExtentWidthChange > 0 || e.ViewportWidthChange < 0) &&
            e.HorizontalOffset + e.ViewportWidth < e.ExtentWidth - 1)
        {
            var prevScrollRight = e.HorizontalOffset - e.HorizontalChange + e.ViewportWidth - e.ViewportWidthChange;
            var prevExtentWidth = e.ExtentWidth - e.ExtentWidthChange;
            if (prevScrollRight > prevExtentWidth - 1)
            {
                scrollViewer_?.ScrollToHorizontalOffset(e.ExtentWidth - e.ViewportWidth);
            }
        }
    }

    [Flags]
    public enum SnappingTypes
    {
        None = 0,
        Bottom = 1,
        Right = 2,
    }
}