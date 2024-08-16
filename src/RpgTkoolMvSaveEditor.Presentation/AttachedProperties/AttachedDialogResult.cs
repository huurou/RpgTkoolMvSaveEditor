using System.Windows;

namespace RpgTkoolMvSaveEditor.Presentation.AttachedProperties;

/// <summary>
/// DialogResultにバインドするための添付プロパティ <para/>
/// Windowの開始タグ内に以下を追記する
/// ap:AttachedDialogResult.DialogResult="{Binding DialogResult.Value}"
/// </summary>
public static class AttachedDialogResult
{
    public static bool? GetDialogResult(DependencyObject obj)
    {
        return (bool?)obj.GetValue(DialogResultProperty);
    }

    public static void SetDialogResult(Window target, bool? value)
    {
        target.SetValue(DialogResultProperty, value);
    }

    public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
        "DialogResult", typeof(bool?), typeof(AttachedDialogResult), new PropertyMetadata(default(bool?),
            (d, e) =>
            {
                if (d is Window window)
                {
                    window.DialogResult = e.NewValue as bool?;
                }
            }
        )
    );
}