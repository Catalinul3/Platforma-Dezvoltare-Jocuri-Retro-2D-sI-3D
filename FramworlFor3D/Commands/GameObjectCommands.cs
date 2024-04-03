using FramworkFor3D._3DObjects;
using FramworkFor3D.Models;
using FramworlFor3D.helpers;
using FramworlFor3D.ViewModels;
using HelixToolkit.Wpf;
using RetroEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.Commands
{
   public class GameObjectCommands:BaseVM
    {
        private readonly MainVM _mainVM;

       
       public GameObjectCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
        }
        private RelayCommand _add3DCube;
        private RelayCommand _add3DSphere;
        private RelayCommand _add3DOther;
        public RelayCommand add3DCube
        {
            get
            {
                if (_add3DCube == null)
                    _add3DCube = new RelayCommand(addCube);
                return _add3DCube;
            }
        }
        private void addCube(object parameter)
        {  
            if (parameter is Viewport3D environment)
            {
                Cube3D cube = new Cube3D
                {color=Brushes.Red,
                };

                environment.Children.Add(cube);
                TranslateTransform3D center= new TranslateTransform3D(1, 0.8, 0);
                cube.Transform= center;
                //MessageBox.Show("Cube " + cube.SideLength);

            }
        }
        public RelayCommand add3DSphere
        {
            get
            {
                if (_add3DSphere == null)
                    _add3DSphere = new RelayCommand(addSphere);
                return _add3DSphere;
            }
        }
        private void addSphere(object parameter)
        {
            if (parameter is Viewport3D environment)
            {
                SphereVisual3D cube = new SphereVisual3D
                {
                   Radius=2,
                   Fill=Brushes.Gray
                };
                environment.Children.Add(cube);
                //MessageBox.Show("Cube " + cube.SideLength);

            }
        }
        public RelayCommand add3DOther
        {
            get
            {
                if (_add3DOther == null)
                    _add3DOther = new RelayCommand(addCylinder);
                return _add3DOther;
            }
        }
        private void addCylinder(object parameter)
        {
            if (parameter is HelixViewport3D environment)
            {
                
               
                //MessageBox.Show("Cube " + cube.SideLength);

            }
        }

    }
}
