using RetroEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetroEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imageState.Source = new BitmapImage(new Uri("../images/create_image.png", UriKind.Relative));
        }
        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imageState.Source = null;
        }
        private void Button_MouseEnterLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imageState.Source = new BitmapImage(new Uri("../images/GameOver.jpg", UriKind.Relative));
        }
    }
}
