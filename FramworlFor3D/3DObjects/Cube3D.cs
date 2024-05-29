using BulletSharp;
using BulletSharp.Math;
using Eco.Persistence;
using FramworkFor3D._3DPhysics;
using FramworkFor3D.Based_Operations;
using FramworkFor3D.helpers;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FramworkFor3D._3DObjects
{
    public class Cube3D : ModelVisual3D, BasedOperation
    {
        #region Cube Morphology
        private Brush _color;
        public Brush color
        {
            get
            {
                return _color;
            }
            set
            {
                this._color = value;
            }
        }
        public int size { get; set; }
        public Point3DCollection vertices { get; set; }
        public Int32Collection indices { get; set; }
        
        private Vector3D _position { get; set; }

        private float velocity { get; set; }
        public float Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                this.velocity = value;
            }
        }
        public Vector3D position
        {
            get
            {
                return _position;
            }
            set
            {
                this._position = value;
            }
        }
 


        #endregion

        #region Create Cube
        public Cube3D()
        { //Adding Light for cube

            DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            ModelVisual3D lightOfCube = new ModelVisual3D();
            size = 2;
            lightOfCube.Content = light;
            velocity = 0f;
          
            

            //Build cube 

     
            
            MeshGeometry3D cubeShape = new MeshGeometry3D();


            cubeShape.Positions.Add(new Point3D(0, 0, 0));
           
            cubeShape.Positions.Add(new Point3D(1, 0, 0));
         
            cubeShape.Positions.Add(new Point3D(0, 1, 0));
          
            cubeShape.Positions.Add(new Point3D(1, 1, 0));
          
            cubeShape.Positions.Add(new Point3D(0, 0, 1));
          
            cubeShape.Positions.Add(new Point3D(1, 0, 1));
           
            cubeShape.Positions.Add(new Point3D(0, 1, 1));
         
            cubeShape.Positions.Add(new Point3D(1, 1, 1));
            

            double scale = 0.4;

            for (int i = 0; i < cubeShape.Positions.Count; i++)
            {
                Point3D originalposition = cubeShape.Positions[i];
                Point3D scaledposition = new Point3D(originalposition.X * scale, originalposition.Y * scale, originalposition.Z * scale);
                cubeShape.Positions[i] = scaledposition;
            }

            cubeShape.TriangleIndices.Add(0);
          
            cubeShape.TriangleIndices.Add(2);
            cubeShape.TriangleIndices.Add(1);
         
            cubeShape.TriangleIndices.Add(1);
            cubeShape.TriangleIndices.Add(2);
            cubeShape.TriangleIndices.Add(3);
    
            cubeShape.TriangleIndices.Add(0);
            cubeShape.TriangleIndices.Add(4);
            cubeShape.TriangleIndices.Add(2);
            
            cubeShape.TriangleIndices.Add(2);
            cubeShape.TriangleIndices.Add(4);
            cubeShape.TriangleIndices.Add(6);
            

            cubeShape.TriangleIndices.Add(0);
            cubeShape.TriangleIndices.Add(1);
            cubeShape.TriangleIndices.Add(4);
            

            cubeShape.TriangleIndices.Add(1);
            cubeShape.TriangleIndices.Add(5);
            cubeShape.TriangleIndices.Add(4);
           
            cubeShape.TriangleIndices.Add(1);
            cubeShape.TriangleIndices.Add(7);
            cubeShape.TriangleIndices.Add(5);
           
            cubeShape.TriangleIndices.Add(1);
            cubeShape.TriangleIndices.Add(3);
            cubeShape.TriangleIndices.Add(7);
            
            cubeShape.TriangleIndices.Add(4);
            cubeShape.TriangleIndices.Add(5);
            cubeShape.TriangleIndices.Add(6);
     
            cubeShape.TriangleIndices.Add(7);
            cubeShape.TriangleIndices.Add(6);
            cubeShape.TriangleIndices.Add(5);
           
            cubeShape.TriangleIndices.Add(2);
            cubeShape.TriangleIndices.Add(6);
            cubeShape.TriangleIndices.Add(3);
           
            cubeShape.TriangleIndices.Add(3);
            cubeShape.TriangleIndices.Add(6);
            cubeShape.TriangleIndices.Add(7);
        

            GeometryModel3D cubeGeometry = new GeometryModel3D();
            cubeGeometry.Geometry = cubeShape;
           
            DiffuseMaterial material = new DiffuseMaterial();
            material.Brush = Brushes.LightGray;
            cubeGeometry.Material = material;
            Model3DGroup lightAndGeometry = new Model3DGroup();
    
            lightAndGeometry.Children.Add(lightOfCube.Content);
            lightAndGeometry.Children.Add(cubeGeometry);

            ModelVisual3D cubeForm = new ModelVisual3D();
            cubeForm.Content = lightAndGeometry;
            position=new Vector3D(cubeForm.Transform.Value.OffsetX, cubeForm.Transform.Value.OffsetY, cubeForm.Transform.Value.OffsetZ);
          
            
            Content = cubeForm.Content;



            //Apply physics to cube
            


        }

       

      
        
     
        #endregion

        #region Based Transformation
        public void Rotate(double angle, Vector3D axis)
        {
            AxisAngleRotation3D axisS = new AxisAngleRotation3D(axis, angle);
            RotateTransform3D rotate = new RotateTransform3D(axisS);
            this.Transform = rotate;
        }

        public void Translate(Vector3D axis)
        {
            if (axis.X != null)
            {
                Vector3D translate = new Vector3D(0.1, 0, 0);
                TranslateTransform3D translateTransform = new TranslateTransform3D(translate);
                this.Transform = translateTransform;
            }
            //translatie pe axa y
            if (axis.Y != null)
            {
                Vector3D translate = new Vector3D(0, 0.1, 0);
                TranslateTransform3D translateTransform = new TranslateTransform3D(translate);
                this.Transform = translateTransform;
            }
            //translatie pe axa z
            if (axis.Z != null)
            {
                Vector3D translate = new Vector3D(0, 0, 0.1);
                TranslateTransform3D translateTransform = new TranslateTransform3D(translate);
                this.Transform = translateTransform;
            }
        }

        public void Scale(Vector3D axis, double scaleFactor)
        {
            if (axis.X != null)
            {
                ScaleTransform3D scale = new ScaleTransform3D(scaleFactor, 1, 1);
                this.Transform = scale;
            }
            //scalare pe axa y cu factorul scaleFactor
            if (axis.Y != null)
            {
                ScaleTransform3D scale = new ScaleTransform3D(1, scaleFactor, 1);
                this.Transform = scale;
            }
            //scalare pe axa z cu factorul scaleFactor
            if (axis.Z != null)
            {
                ScaleTransform3D scale = new ScaleTransform3D(1, 1, scaleFactor);
                this.Transform = scale;
            }
        }

       public void Fall(double velocity)
        {
            Vector3D translate = new Vector3D(0, 0, velocity);
            TranslateTransform3D translateTransform = new TranslateTransform3D(translate);
            this.Transform = translateTransform;
        }
       

        #endregion


    }
}
