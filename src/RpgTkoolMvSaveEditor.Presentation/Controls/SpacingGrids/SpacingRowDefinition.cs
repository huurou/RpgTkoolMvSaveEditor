using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.SpacingGrids;

public class SpacingRowDefinition : RowDefinition, ISpacing
{
    public double Spacing { get => Height.Value; set => Height = new GridLength(value, GridUnitType.Pixel); }

    public SpacingRowDefinition(double height)
    {
        Height = new GridLength(height, GridUnitType.Pixel);
    }
}
