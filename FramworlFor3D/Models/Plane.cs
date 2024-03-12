using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
namespace FramworkFor3D.Models
{
    public class Plane : ModelVisual3D
    {
        private MeshGeometry3D mesh;
        private GeometryModel3D geometry;

        public Plane() {
            CreatePlane();
        }
        public void CreatePlane()
        {
            DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            ModelVisual3D grid = new ModelVisual3D();
            ModelVisual3D lightVisual = new ModelVisual3D();
            lightVisual.Content = light;
            double borderThickness = 0.9;


            mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(1, 1, 0));
            mesh.Positions.Add(new Point3D(-1, 1, 0));
            mesh.Positions.Add(new Point3D(-1, -1, 0));
            mesh.Positions.Add(new Point3D(1, -1, 0));

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            geometry = new GeometryModel3D();
            geometry.Geometry = mesh;
            geometry.Material = new DiffuseMaterial(Brushes.Red);



            Model3DGroup modelGroup = new Model3DGroup();
            modelGroup.Children.Add(geometry);

            ModelVisual3D cellVisual = new ModelVisual3D();
            cellVisual.Content = modelGroup;

        }
    }
}

