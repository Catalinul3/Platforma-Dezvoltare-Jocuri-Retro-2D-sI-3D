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
        private Irregular3DObject obj;
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
                Sphere3D sphere = new Sphere3D();
                environment.Children.Add(sphere);
                TranslateTransform3D center = new TranslateTransform3D(1, 0.8, 0);
                sphere.Transform = center;
               
             

            }
        }
        public RelayCommand add3DOther
        {
            get
            {
                if (_add3DOther == null)
                    _add3DOther = new RelayCommand(addOther);
                return _add3DOther;
            }
        }
        private void addOther(object parameter)
        {
            if (parameter is Viewport3D environment)
            {
                string fileName = LoadFileDialog("Select a 3d model");

                if (fileName != null)
                {
                    obj = new Irregular3DObject(fileName);
                    MessageBox.Show("succes");


                    Transform3DGroup transformGroup = new Transform3DGroup();

                    RotateTransform3D rotate = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
                    transformGroup.Children.Add(rotate);
                    TranslateTransform3D center = new TranslateTransform3D(1, 1, 0);
                    transformGroup.Children.Add(center);
                    obj.Transform = transformGroup;
                    environment.Children.Add(obj);
                }

                //MessageBox.Show("Cube " + cube.SideLength);

            }
        }

    }
}
