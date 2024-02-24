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
            
        }

        //private Point lastPoint;
        //private double angleX = 0;
        //private double angleY = 0;
        //private bool isLeftOnXaxis(MouseEventArgs e)
        //{
        //    var currentPosition = e.GetPosition(canvas);
        //    if (currentPosition.X < lastPoint.X)
        //    {
        //        lastPoint.X = currentPosition.X;
        //        return true;
        //    }
        //    lastPoint.X = currentPosition.X;
        //    return false;
        //}
        //private bool isUpOnYaxis(MouseEventArgs e)
        //{
        //    var currentPosition = e.GetPosition(canvas);
        //    if (currentPosition.Y < lastPoint.Y)
        //    {
        //        lastPoint.Y = currentPosition.Y;
        //        return true;
        //    }
        //    lastPoint.Y = currentPosition.Y;
        //    return false;
        //}
        //bool holdMouseL = false;
        //bool holdMouseR = false;
        //protected virtual void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    holdMouseL = false;
        //    (sender as UIElement).CaptureMouse();
        //}
        //protected virtual void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    holdMouseR = false;
        //    (sender as UIElement).CaptureMouse();
        //}
        //protected virtual void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    holdMouseR = true;
        //    (sender as UIElement).CaptureMouse();
        //}

        //private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    holdMouseL = true;
        //    (sender as UIElement).CaptureMouse();
        //}
        //private void OnMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (holdMouseL)
        //    {
        //        Point point = e.GetPosition(canvas);

        //        if (xAxis.X1 > 100 || yAxis.X1 > 100 || zAxis.X1 > 100)
        //        {
        //            xAxis.X1 = 100;
        //            yAxis.X1 = 100;
        //            zAxis.X1 = 100;
        //            xAxisText.Text = "X" + xAxis.X1.ToString();
        //            yAxisText.Text = "Y" + yAxis.X1.ToString();
        //            zAxisText.Text = "Z" + zAxis.X1.ToString();
        //        }
        //        if (xAxis.X1 < -90 || yAxis.X1 < -90 || zAxis.X1 < -90)
        //        {
        //            xAxis.X1 = 100;
        //            xAxisText.Text = "X" + xAxis.X1.ToString();
        //            yAxisText.Text = "Y" + yAxis.X1.ToString();
        //            zAxisText.Text = "Z" + zAxis.X1.ToString();
        //            yAxis.X1 = 100;
        //            zAxis.X1 = 100;
        //        }
        //        else
        //        {
        //            if (isLeftOnXaxis(e))
        //            {
        //                angleX -= 5;
        //                xAxis.X1 -= 5;
        //                xAxisText.Text = "X" + xAxis.X1.ToString(); yAxisText.Text = "Y" + yAxis.X1.ToString();
        //                zAxisText.Text = "Z" + zAxis.X1.ToString();
        //                yAxis.X1 -= 5;
        //                zAxis.X1 -= 5;
        //                AxisAngleRotation3D rotationX = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angleX);
        //                RotateTransform3D rotateTransformX = new RotateTransform3D(rotationX);
        //                Cub.Transform = rotateTransformX;
        //            }
        //            else
        //            {
        //                angleX += 5;
        //                xAxis.X1 += 5;
        //                xAxisText.Text = "X" + xAxis.X1.ToString();
        //                yAxisText.Text = "Y" + yAxis.X1.ToString();
        //                zAxisText.Text = "Z" + zAxis.X1.ToString();
        //                yAxis.X1 += 5;
        //                zAxis.X1 += 5;
        //                AxisAngleRotation3D rotationX = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angleX);
        //                RotateTransform3D rotateTransformX = new RotateTransform3D(rotationX);
        //                Cub.Transform = rotateTransformX;
        //            }
        //        }
        //    }
        //    if (holdMouseR)
        //    {
        //        if (xAxis.Y1 > 180 || yAxis.Y1 > 180 || zAxis.Y1 > 180)
        //        {
        //            xAxis.Y1 = 180;
        //            yAxis.Y1 = 180;
        //            zAxis.Y1 = 180;
        //            xAxisText.Text = "X" + xAxis.Y1.ToString();
        //            yAxisText.Text = "Y" + yAxis.Y1.ToString();
        //            zAxisText.Text = "Z" + zAxis.Y1.ToString();
        //        }
        //        if (xAxis.Y1 < -90 || yAxis.Y1 < -90 || zAxis.Y1 < -90)
        //        {
        //            xAxis.Y1 = -90;
        //            yAxis.Y1 = -90;
        //            zAxis.Y1 = -90;
        //            xAxisText.Text = "X" + xAxis.Y1.ToString();
        //            yAxisText.Text = "Y" + yAxis.Y1.ToString();
        //            zAxisText.Text = "Z" + zAxis.Y1.ToString();
        //        }

        //        if (isUpOnYaxis(e))
        //        {
        //            angleY -= 5;
        //            AxisAngleRotation3D rotationy = new AxisAngleRotation3D(new Vector3D(1, 0, 0), angleY);
        //            RotateTransform3D rotateTransformy = new RotateTransform3D(rotationy);
        //            Cub.Transform = rotateTransformy;

        //            xAxis.Y1 -= 5;
        //            yAxis.Y1 -= 5;
        //            zAxis.Y1 -= 5;
        //            xAxisText.Text = "X" + xAxis.Y1.ToString();
        //            yAxisText.Text = "Y" + yAxis.Y1.ToString();
        //            zAxisText.Text = "Z" + zAxis.Y1.ToString();
        //        }
        //        else
        //        {
        //            angleY += 5;
        //            AxisAngleRotation3D rotationy = new AxisAngleRotation3D(new Vector3D(1, 0, 0), angleY);
        //            RotateTransform3D rotateTransformy = new RotateTransform3D(rotationy);
        //            Cub.Transform = rotateTransformy;

        //            xAxis.Y1 += 5;
        //            yAxis.Y1 += 5;
        //            zAxis.Y1 += 5;
        //            xAxisText.Text = "X" + xAxis.Y1.ToString();
        //            yAxisText.Text = "Y" + yAxis.Y1.ToString();
        //            zAxisText.Text = "Z" + zAxis.Y1.ToString();
        //        }
        //    }
        //}


    }
}
