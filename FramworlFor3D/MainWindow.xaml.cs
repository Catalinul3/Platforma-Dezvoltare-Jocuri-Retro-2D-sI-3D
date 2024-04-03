﻿using FramworkFor3D.Based_Operations;
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
        #region Variables
        private TrackBall track;
        double deltaX = .1;
        double deltaY = .1;
        private bool isMouseCaptured = false;
        private bool isKeyPressed = false;
        private Point lastClick;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
           CreateGrid();
            track = new TrackBall();
        }

        #region Rotate

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
            this.Cursor = Cursors.Arrow;
        }
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseCaptured)
            { //convertirea punctelor 2D la spațiul de coordonate sferic 3D 
                double width = environment.RenderSize.Width;
                double height = environment.RenderSize.Height;
                Point currentClick = e.GetPosition((IInputElement)sender);
                Point rotateClick = new Point(width / 2, height / 2);
                Vector3D rotateClickSphereCoordinates = track.ConvertToSphereCoordinates(rotateClick, width, height);
                Vector3D currentClickSphereCoordinates = track.ConvertToSphereCoordinates(currentClick, width, height);

                //determinarea axei pe care o formeaza punctul din mijlocul si punctul curent al mouse-ului
                Vector3D axis = Vector3D.CrossProduct(rotateClickSphereCoordinates, currentClickSphereCoordinates);

                //determinarea unghiului dintre cele doua puncte;
                double theta = Vector3D.AngleBetween(rotateClickSphereCoordinates, currentClickSphereCoordinates);

                Quaternion delta = new Quaternion(axis, -theta);

                //rotatia 

                Matrix3D rotate = new Matrix3D();
                rotate.Rotate(delta);

                environment.Camera.Transform = new MatrixTransform3D(rotate);


            }

        }
        #endregion

        #region Move
        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    isKeyPressed = true;
                    KeyA(); break;
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


            Matrix3D camera = ((MatrixTransform3D)environment.Camera.Transform).Matrix;
            Matrix3D translateMatrix = track.MoveCameraOnXaxis(-deltaX,camera);
            
           
            
            environment.Camera.Transform = new MatrixTransform3D(translateMatrix);


        }
        private void KeyD()
        {

            Matrix3D camera = ((MatrixTransform3D)environment.Camera.Transform).Matrix;
            Matrix3D translateMatrix = track.MoveCameraOnXaxis(deltaX, camera);



            environment.Camera.Transform = new MatrixTransform3D(translateMatrix);

        }
        private void KeyW()
        {

            Matrix3D camera = ((MatrixTransform3D)environment.Camera.Transform).Matrix;
            Matrix3D translateMatrix = track.MoveCameraOnYaxis(deltaY, camera);



            environment.Camera.Transform = new MatrixTransform3D(translateMatrix);

        }
        private void KeyS()
        {


            Matrix3D camera = ((MatrixTransform3D)environment.Camera.Transform).Matrix;
            Matrix3D translateMatrix = track.MoveCameraOnYaxis(-deltaY, camera);



            environment.Camera.Transform = new MatrixTransform3D(translateMatrix);


        }
        #endregion

        #region Zoom
        private void MouseWheel(object sender,MouseWheelEventArgs e)
        {
            double zoom = 0;
            if(e.Delta>0)
            {
                zoom = 0.1;
            }
            else
            {
                zoom = -0.1;
            }

            Zoom(zoom);
        }

        private void Zoom(double zoom)
        { Matrix3D zoomTranslate = track.ZoomCamera(zoom);
            Matrix3D camera = ((MatrixTransform3D)environment.Camera.Transform).Matrix;
            Matrix3D cameraZoom = Matrix3D.Multiply(camera, zoomTranslate);
            environment.Camera.Transform = new MatrixTransform3D(cameraZoom);

        }
        #endregion

        #region Helpers
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
        #endregion

    }
}
