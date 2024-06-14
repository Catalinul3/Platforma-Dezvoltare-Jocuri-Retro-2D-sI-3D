using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Point = System.Windows.Point;

namespace FramworkFor3D.Based_Operations
{
    public class TrackBall
    {
      
        public TrackBall()
        {
           
        
        }

        #region RotateCamera
        public Vector3D ConvertToSphereCoordinates(Point click,double width,double height)
        {   //normalizarea coordonatelor click-ului
            double x= click.X/(width/2);
            double y= click.Y/(height/2);
            x = x - 1;
            y = 1 - y;
            //scoatem z din ecuatia sqrt(x^2+y^2+z^2)=1
            double zSquare=1-Math.Pow(x,2)-Math.Pow(y,2);
            double z=zSquare >0 ? Math.Sqrt(zSquare) : 0;
            Vector3D newPoint=new Vector3D(x,y,z);
           newPoint.Normalize();
            return newPoint;
        }
        public Matrix3D RotateCamera(Point lastClick,Point currentClick,Matrix3D camera,Viewport3D environment,ModelVisual3D centerOfGrid)
        {
            double width = environment.RenderSize.Width;
            double height = environment.RenderSize.Height;
            


            Point rotate = new Point(width / 2, height / 2);
        
             Vector3D rotateClickSphereCoordinates = ConvertToSphereCoordinates(rotate,width,height);
            Vector3D currentClickSphereCoordinates = ConvertToSphereCoordinates(currentClick, width, height);

            //determinarea axei pe care o formeaza punctul din mijlocul si punctul curent al mouse-ului
            Vector3D axis = Vector3D.CrossProduct(rotateClickSphereCoordinates, currentClickSphereCoordinates);

            //determinarea unghiului dintre cele doua puncte;
            double theta = Vector3D.AngleBetween(rotateClickSphereCoordinates, currentClickSphereCoordinates);

            Quaternion delta = new Quaternion(axis, -theta);

            //rotatia 

            Matrix3D matrix = new Matrix3D();
            matrix.Rotate(delta);
            return matrix;
        }
        #endregion
        public Vector3D normalizeCoordinates(Vector3D coordinate)
        {
            coordinate.Normalize();
            Vector3D newCoordinate=new Vector3D(1-coordinate.X,1-coordinate.Y,1-coordinate.Z);
            return coordinate;
        }

        #region MoveCamera
        public Matrix3D MoveCameraOnXaxis(double deltaX,Matrix3D camera)
        {
            
            Vector3D translate = new Vector3D(deltaX, 0, 0);
            //Matrix3D translateMatrix = new Matrix3D();
            camera.Translate(translate);
            return camera;
        }
        public Matrix3D MoveCameraOnYaxis(double deltaY,Matrix3D camera)
        {

            Vector3D translate = new Vector3D(0, deltaY, 0);
           
            camera.Translate(translate);
            return camera;
        }
        #endregion

        #region ZoomCamera
        public Matrix3D ZoomCamera(double deltaZoom)
        {
            Matrix3D zoom=new Matrix3D();
            zoom.Translate(new Vector3D(0, deltaZoom, deltaZoom));
            return zoom;

        }
        #endregion
    }
}
