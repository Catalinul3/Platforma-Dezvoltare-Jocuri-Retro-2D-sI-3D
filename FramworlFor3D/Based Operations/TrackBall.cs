using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public Vector3D ConvertToSphereCoordinates(Point click,double width,double height)
        {
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
       public Matrix3D MoveCameraOnXaxis(double deltaX)
        {
            
            Vector3D translate = new Vector3D(deltaX, 0, 0);
            Matrix3D translateMatrix = new Matrix3D();
            translateMatrix.Translate(translate);
            return translateMatrix;
        }
        public Matrix3D MoveCameraOnYaxis(double deltaY)
        {

            Vector3D translate = new Vector3D(0, deltaY, 0);
            Matrix3D translateMatrix = new Matrix3D();
            translateMatrix.Translate(translate);
            return translateMatrix;
        }
        public Matrix3D ZoomCamera(double deltaZoom)
        {
            Matrix3D zoom=new Matrix3D();
            zoom.Translate(new Vector3D(0, 0, deltaZoom));
            return zoom;

        }
    }
}
