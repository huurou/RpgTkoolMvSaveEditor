using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls
{
    /// <summary>
    /// ActorControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ActorControl : UserControl
    {
        #region Dependency Property

        internal ActorVM Source
        {
            get => (ActorVM)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(ActorVM), typeof(ActorControl), new FrameworkPropertyMetadata(default(ActorVM), (d, e) =>
            {
                if (d is ActorControl self &&
                    e.NewValue is ActorVM value)
                {
                    self.HP = value.HP;
                    self.MP = value.MP;
                    self.TP = value.TP;
                    self.Exp = value.Exp;
                }
            }));

        public int HP
        {
            get => (int)GetValue(HPProperty);
            set => SetValue(HPProperty, value);
        }

        // Using a DependencyProperty as the backing store for HP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HPProperty = DependencyProperty.Register(
            "HP", typeof(int), typeof(ActorControl), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                if (d is ActorControl self &&
                    e.NewValue is int value)
                {
                    self.Source.HP = value;
                }
            }));

        public int MP
        {
            get => (int)GetValue(MPProperty);
            set => SetValue(MPProperty, value);
        }

        // Using a DependencyProperty as the backing store for MP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MPProperty = DependencyProperty.Register(
            "MP", typeof(int), typeof(ActorControl), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                if (d is ActorControl self &&
                    e.NewValue is int value)
                {
                    self.Source.MP = value;
                }
            }));

        public int TP
        {
            get => (int)GetValue(TPProperty);
            set => SetValue(TPProperty, value);
        }

        // Using a DependencyProperty as the backing store for TP.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TPProperty = DependencyProperty.Register(
            "TP", typeof(int), typeof(ActorControl), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                if (d is ActorControl self &&
                    e.NewValue is int value)
                {
                    self.Source.TP = value;
                }
            }));

        public int Exp
        {
            get => (int)GetValue(ExpProperty);
            set => SetValue(ExpProperty, value);
        }

        // Using a DependencyProperty as the backing store for Exp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpProperty = DependencyProperty.Register(
            "Exp", typeof(int), typeof(ActorControl), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                if (d is ActorControl self &&
                    e.NewValue is int value)
                {
                    self.Source.Exp = value;
                }
            }));

        #endregion Dependency Property

        public ActorControl()
        {
            InitializeComponent();
        }
    }
}