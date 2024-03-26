using FramworkFor3D.Based_Operations;
using FramworkFor3D.helpers;
using FramworkFor3D.Models;
using FramworlFor3D.ViewModels;
using MDriven.WPF.Media3D;
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
        private TrackBall track;
        double deltaX= 0.1;

        public MainWindow()
        {

            InitializeComponent();
            CreateGrid();
            track = new TrackBall();
          

        }
        private bool isMouseCaptured = false;
        private bool isAPressed = false;
        private bool isKeyPressed = false;
        private Point lastClick;
        

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).CaptureMouse();
            isMouseCaptured = true;
            double width = environment.RenderSize.Width;
            double height = environment.RenderSize.Height;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Cursor = Cursors.ScrollAll;
                lastClick = e.GetPosition((IInputElement)sender);
                Vector3D sphere = track.ConvertToSphereCoordinates(lastClick, width, height);
               
                environment.MouseMove += MouseMove;


            }
            else
            {
                environment.MouseUp += MouseUp;
            }

        }
        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
            isMouseCaptured = false;
        }
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseCaptured)
            { //convertirea punctelor 2D la spațiul de coordonate sferic 3D 
                double width = environment.RenderSize.Width;
                double height = environment.RenderSize.Height;
                Point currentClick = e.GetPosition((IInputElement)sender);
                Point rotateClick= new Point(width/ 2, height / 2);
                Vector3D lastClickSphereCoordinates = track.ConvertToSphereCoordinates(rotateClick, width, height);
                Vector3D currentClickSphereCoordinates = track.ConvertToSphereCoordinates(currentClick, width, height);

                //determinarea axei pe care o formeaza cele doua puncte ale mouse-ului unde se va face rotatia camerei
                Vector3D axis = Vector3D.CrossProduct(lastClickSphereCoordinates, currentClickSphereCoordinates);

                //determinarea unghiului dintre cele doua puncte ale mouse-ului
                double theta = Vector3D.AngleBetween(lastClickSphereCoordinates, currentClickSphereCoordinates);

                Quaternion delta = new Quaternion(axis, -theta);

                //rotatia 
               
                Matrix3D rotate = new Matrix3D();
                rotate.Rotate(delta);

                environment.Camera.Transform = new MatrixTransform3D(rotate);
                
                
            }
           
        }
        private void KeyDown(object sender, KeyEventArgs e)
        {
           switch(e.Key)
            {
                case Key.A:
                    isAPressed = true;
                    KeyA();break;
                case Key.D:
                    isKeyPressed = true;
                    KeyD(); break;
                case Key.W:
                    isKeyPressed = true;
                    KeyW(); break;
                case Key.S:
                    isKeyPressed = true;
                    KeyS(); break;                
                default: break;
            }
            
        }
        private void KeyA()
        {

            if (isAPressed)
            {
                Matrix3D translateMatrix =track.MoveCameraOnXaxis(-deltaX);
                deltaX -= 0.1;
                environment.Camera.Transform = new MatrixTransform3D(translateMatrix);
               
            }
        }
        private void KeyD()
        {


            Matrix3D translateMatrix = track.MoveCameraOnXaxis(deltaX);
            deltaX += 0.1;
            environment.Camera.Transform = new MatrixTransform3D(translateMatrix);
            
        }
        private void KeyW()
        {


            Matrix3D translateMatrix = track.MoveCameraOnYaxis(.1);

            environment.Camera.Transform = new MatrixTransform3D(translateMatrix);

        }
        private void KeyS()
        {


            Matrix3D translateMatrix = track.MoveCameraOnYaxis(-.1);

            environment.Camera.Transform = new MatrixTransform3D(translateMatrix);

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
      
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            environment.Focus();
        }


    }
}
