using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Updater.Controls
{
    /// <summary>
    /// Interaction logic for SliderControl.xaml
    /// </summary>
    public partial class ShopNewsControl : UserControl
    {
        public ShopNewsControl()
        {
            InitializeComponent();
        }

        private void RecommendedButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://r2dispel.ru/");
        }
    }
}
