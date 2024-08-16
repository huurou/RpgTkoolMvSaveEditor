using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.SpacingStackPanels;

public class SpacingStackPanel : StackPanel
{
    public double Spacing { get => (double)GetValue(SpacingProperty); set => SetValue(SpacingProperty, value); }
    public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(SpacingStackPanel), new FrameworkPropertyMetadata(default));

    protected override Size MeasureOverride(Size availableSize)
    {
        var stackDesiredSize = new Size();
        var isHorizontal = Orientation == Orientation.Horizontal;
        foreach (UIElement child in InternalChildren)
        {
            child.Measure(availableSize);
            if (isHorizontal)
            {
                stackDesiredSize.Width += child.DesiredSize.Width + Spacing;
                stackDesiredSize.Height = Math.Max(stackDesiredSize.Height, child.DesiredSize.Height);
            }
            else
            {
                stackDesiredSize.Width = Math.Max(stackDesiredSize.Width, child.DesiredSize.Width);
                stackDesiredSize.Height += child.DesiredSize.Height + Spacing;
            }
        }
        // スペースの最後の部分を削除
        if (isHorizontal)
        {
            stackDesiredSize.Width -= Spacing;
        }
        else
        {
            stackDesiredSize.Height -= Spacing;
        }
        return stackDesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var offset = 0D;
        foreach (UIElement child in InternalChildren)
        {
            if (Orientation == Orientation.Horizontal)
            {
                child.Arrange(new Rect(offset, 0, child.DesiredSize.Width, finalSize.Height));
                offset += child.DesiredSize.Width + Spacing;
            }
            else
            {
                child.Arrange(new Rect(0, offset, finalSize.Width, child.DesiredSize.Height));
                offset += child.DesiredSize.Height + Spacing;
            }
        }

        return finalSize;
    }
}
