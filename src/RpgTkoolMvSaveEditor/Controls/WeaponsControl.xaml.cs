using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RpgTkoolMvSaveEditor.Controls
{
    /// <summary>
    /// WeaponsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WeaponsControl : UserControl
    {
        #region Dependency Property

        internal ObservableCollection<WeaponVM> Source
        {
            get => (ObservableCollection<WeaponVM>)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(ObservableCollection<WeaponVM>), typeof(WeaponsControl), new FrameworkPropertyMetadata(default(ObservableCollection<WeaponVM>)));

        #endregion Dependency Property

        public WeaponsControl()
        {
            InitializeComponent();
        }

        private void MenuItem_Set1_Click(object sender, RoutedEventArgs e)
        {
            foreach (WeaponVM weapon in DataGrid_Items.SelectedItems)
            {
                weapon.Count = 1;
            }
        }

        private void MenuItem_Set99_Click(object sender, RoutedEventArgs e)
        {
            foreach (WeaponVM weapon in DataGrid_Items.SelectedItems)
            {
                weapon.Count = 99;
            }
        }
    }
}