using FramworkFor3D.Based_Operations;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DObjects
{
    public class Cube3D : ModelVisual3D,BasedOperation
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

        private List<ModelVisual3D> cube { get; set; }
        #endregion

        public Cube3D()
        { //Adding Light for cube
            cube = new List<ModelVisual3D>();
            DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            ModelVisual3D lightOfCube = new ModelVisual3D();
            size = 2;
            lightOfCube.Content = light;
            double newSize = 1 / size;

            //Build cube 

            ModelVisual3D cubeConstruction = new ModelVisual3D();
            MeshGeometry3D cubeShape = new MeshGeometry3D();
         
           
            cubeShape.Positions.Add(new Point3D(0, 0, 0));
            cubeShape.Positions.Add(new Point3D(1, 0, 0));
            cubeShape.Positions.Add(new Point3D(0, 1, 0));
            cubeShape.Positions.Add(new Point3D(1, 1, 0));
            cubeShape.Positions.Add(new Point3D(0, 0, 1));
            cubeShape.Positions.Add(new Point3D(1, 0, 1));
            cubeShape.Positions.Add(new Point3D(0, 1, 1));
            cubeShape.Positions.Add(new Point3D(1, 1, 1));
            double scale = 0.3; 

            for (int i = 0; i < cubeShape.Positions.Count; i++)
            {
                Point3D originalPosition = cubeShape.Positions[i];
                Point3D scaledPosition = new Point3D(originalPosition.X * scale, originalPosition.Y * scale, originalPosition.Z * scale);
                cubeShape.Positions[i] = scaledPosition;
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
            material.Brush = Brushes.Red;
            cubeGeometry.Material = material;
            Model3DGroup lightAndGeometry= new Model3DGroup();
            lightAndGeometry.Children.Add(lightOfCube.Content);
            lightAndGeometry.Children.Add(cubeGeometry);
            
            ModelVisual3D cubeForm=new ModelVisual3D();
            cubeForm.Content = lightAndGeometry;

            Content=cubeForm.Content;



        }

        public void Rotate(double angle)
        {
            throw new NotImplementedException();
        }

        public void Translate()
        {
            throw new NotImplementedException();
        }

        public void Scale()
        {
            throw new NotImplementedException();
        }
    }
}
