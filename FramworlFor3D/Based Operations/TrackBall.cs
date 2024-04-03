﻿using System;
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
        #endregion

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
            zoom.Translate(new Vector3D(0, 0, deltaZoom));
            return zoom;

        }
        #endregion
    }
}
