using RpgTkoolMvSaveEditor.Application;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls
{
    /// <summary>
    /// ActorsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ActorsControl : UserControl
    {
        #region Dependency Property

        internal ObservableCollection<ActorVM> Source
        {
            get => (ObservableCollection<ActorVM>)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(ObservableCollection<ActorVM>), typeof(ActorsControl), new FrameworkPropertyMetadata(default(ObservableCollection<ActorVM>), (d, e) =>
            {
                if (d is ActorsControl self &&
                    e.NewValue is ObservableCollection<ActorVM> value)
                {
                    if (value.Any()) self.TabContorl_Actors.SelectedIndex = 0;
                }
            }));

        #endregion Dependency Property

        public ActorsControl()
        {
            InitializeComponent();
        }
    }

    internal class ActorVM : NotificationObject
    {
        private string name_ = "";
        private int hp_;
        private int mp_;
        private int exp_;
        private int tp_;

        public string Name { get => name_; set => SetProperty(ref name_, value); }
        public int HP { get => hp_; set => SetProperty(ref hp_, value); }
        public int MP { get => mp_; set => SetProperty(ref mp_, value); }
        public int TP { get => tp_; set => SetProperty(ref tp_, value); }
        public int Exp { get => exp_; set => SetProperty(ref exp_, value); }

        public ActorVM(Actor actor)
        {
            name_ = actor.Name;
            hp_ = actor.HP;
            mp_ = actor.MP;
            tp_ = actor.TP;
            exp_ = actor.Exp;
        }
    }
}