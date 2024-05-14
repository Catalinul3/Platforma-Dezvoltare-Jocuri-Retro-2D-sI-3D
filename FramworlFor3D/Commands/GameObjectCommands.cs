using Eco.UmlRt;
using FramworkFor3D._3DObjects;
using FramworkFor3D._3DPhysics;
using FramworkFor3D.helpers;
using FramworkFor3D.Models;
using FramworlFor3D.helpers;
using FramworlFor3D.ViewModels;

using RetroEngine.ViewModels;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace FramworkFor3D.Commands
{
   public class GameObjectCommands:BaseVM
    {
        private readonly  MainVM _mainVM;

       
       public GameObjectCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
        }
        private RelayCommand _add3DCube;
        private RelayCommand _add3DSphere;
        private RelayCommand _add3DOther;
        private Irregular3DObject obj;
        public System.Windows.Shapes.Rectangle cube2D;
        
        Cube3D cube;
        Sphere3D sphere;

        #region Create Models 
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
                cube = new Cube3D
                { color = Brushes.Red,
                };
               
                UIElement3D cubeInteractive= Cube3DInteractive.ConvertToUI(cube);
                environment.Children.Add(cubeInteractive);
                TranslateTransform3D center = new TranslateTransform3D(2, 1.5, 0);
                cubeInteractive.MouseRightButtonDown +=(s,e)=>CubePressed(s,e,environment);
                cubeInteractive.Transform = center;
                
                // cube2D.Margin= margins.Margin;

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
                
                sphere = new Sphere3D();
                UIElement3D sphereInteractive = Cube3DInteractive.ConvertToUI(sphere);

                environment.Children.Add(sphereInteractive);
                TranslateTransform3D center = new TranslateTransform3D(2, 1.5, 0);
               
               
              

                sphereInteractive.MouseRightButtonDown += (s, e) => SpherePressed(s, e, environment);
                sphereInteractive.Transform = center;



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
                   


                    Transform3DGroup transformGroup = new Transform3DGroup();
                    UIElement3D objInteractive = Cube3DInteractive.ConvertToUI(obj);
                    //RotateTransform3D rotate = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
                    //transformGroup.Children.Add(rotate);
                    obj.Rotate(90, new Vector3D(1, 0, 0));
                    transformGroup.Children.Add(obj.Transform);
                    TranslateTransform3D center = new TranslateTransform3D(2, 1.5, 0);
                    transformGroup.Children.Add(center);
                    obj.Translate(new Vector3D(0, 0, 1));
                    transformGroup.Children.Add(obj.Transform);
                   //obj.Scale(new Vector3D(0, 0, 1), 0.5);
                   // transformGroup.Children.Add(obj.Transform);


                    objInteractive.Transform = transformGroup;
                 
                    objInteractive.MouseRightButtonDown += (s, e) => ObjectPressed(s, e, environment);
                  
                    environment.Children.Add(objInteractive);
                }

                //MessageBox.Show("Cube " + cube.SideLength);

            }
        }
        #endregion

        #region Base Events
        private void ObjectPressed(object sender, MouseEventArgs e, Viewport3D environment)
        {
            MessageBox.Show("Object pressed");
        }
        private void CubePressed(object sender, RoutedEventArgs e, Viewport3D environment)
        {

            MessageBox.Show("Cube pressed");



        }
        private void SpherePressed(object sender, RoutedEventArgs e, Viewport3D environment)
        {

            MessageBox.Show("Sphere pressed");



        }
        private void Cube3D_MouseLeftDown(object sender, MouseEventArgs e)
        {
            ShowLines();
        }

        public void ShowLines()
        {   Viewport3D scene= new Viewport3D();
            GeneralTransform3DTo2D transform = cube.TransformToAncestor(scene);
            MeshGeometry3D geometry = (MeshGeometry3D)((GeometryModel3D)cube.Content).Geometry;
            System.Windows.Shapes.Rectangle marg = new System.Windows.Shapes.Rectangle();
            Rect allMargins = Rect.Empty;
            if (transform != null)
            {
                for (int i = 0; i < geometry.TriangleIndices.Count; i++)
                {
                    Polygon highlight = new Polygon
                    {
                        Stroke = Brushes.Orange,
                        StrokeThickness = 0.25,
                    };
                    var tr = ((GeometryModel3D)cube.Content).Transform;
                    highlight.Points.Add(transform.Transform(tr.Transform(geometry.Positions[geometry.TriangleIndices[i++]])));
                    highlight.Points.Add(transform.Transform(tr.Transform(geometry.Positions[geometry.TriangleIndices[i++]])));
                    highlight.Points.Add(transform.Transform(tr.Transform(geometry.Positions[geometry.TriangleIndices[i++]])));
                    foreach (System.Windows.Point point in highlight.Points)
                    {
                        allMargins.Union(point);
                    }
                }
            }
         
            marg.Width = allMargins.Width;
            marg.Height = allMargins.Height;
            marg.Margin = new Thickness(allMargins.Left, allMargins.Top, 0, 0);

        }
    }
    #endregion
}

