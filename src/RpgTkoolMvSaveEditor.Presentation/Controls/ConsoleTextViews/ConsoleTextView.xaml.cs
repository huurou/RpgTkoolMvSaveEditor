using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews.ConsoleTextItems;

namespace RpgTkoolMvSaveEditor.Presentation.Controls.ConsoleTextViews;

/// <summary>
/// ConsoleTextView.xaml の相互作用ロジック
/// </summary>
public partial class ConsoleTextView : UserControl
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private int capaticy_ = 100;
    private readonly Queue<ConsoleTextItem> itemQueue_ = new(100);

    public TextDecorationCollection TextDecorations { get => (TextDecorationCollection)GetValue(TextDecorationsProperty); set => SetValue(TextDecorationsProperty, value); }

    public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register(
        nameof(TextDecorations), typeof(TextDecorationCollection), typeof(ConsoleTextView), new FrameworkPropertyMetadata(null));

    public ConsoleTextView()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.Property.Name));
    }

    public void AppendConsoleItem(ConsoleTextItem item)
    {
        while (itemQueue_.Count >= capaticy_)
        {
            var removed = itemQueue_.Dequeue();
            consoleDoc.Blocks.Remove(removed.Paragraph);
            removed.BindControlProperties(null);
            removed.Dispose();
        }

        item.BindControlProperties(this);
        itemQueue_.Enqueue(item);
        consoleDoc.Blocks.Add(item.Paragraph);
    }

    [DefaultValue(100)]
    public int ConsoleItemCapacity
    {
        get { return capaticy_; }
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
            if (value < capaticy_)
            {
                while (itemQueue_.Count > value)
                {
                    consoleDoc.Blocks.Remove(itemQueue_.Dequeue().Paragraph);
                }
            }
            capaticy_ = value;
        }
    }

    public void ClearConsoleItems()
    {
        foreach (var item in itemQueue_)
        {
            item.BindControlProperties(null);
            item.Dispose();
        }
        itemQueue_.Clear();
        consoleDoc.Blocks.Clear();
    }
}
