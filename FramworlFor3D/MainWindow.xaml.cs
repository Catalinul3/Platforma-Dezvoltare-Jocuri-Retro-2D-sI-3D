using FramworkFor3D.helpers;
using FramworkFor3D.Models;
using FramworlFor3D.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FramworlFor3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();
            CreateGrid();
           


        }

        private void Environment_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateGrid()
        {
            Scenes sc = new Scenes();
            List<ModelVisual3D> root = sc.getGrid();
            ObservableCollection<ModelVisual3D> grid = new ObservableCollection<ModelVisual3D>(root);
            foreach (var item in grid)
            {
                environment.Children.Add(item);
            }
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            bool holding = true;
            //MessageBox.Show("You press" + e.Key);
            if (DataContext is MainVM viewModel)
            { if (holding == true)
                {
                    if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    {
                        viewModel.RotateCommand.Execute(e.Key);
                    }
                    else
                    {
                        holding= false;
                    }
                }
            }

        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            environment.Focus();
        }


    }
}
