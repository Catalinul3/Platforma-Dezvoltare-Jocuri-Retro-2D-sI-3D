using Eco.ModelValidation;
using FramworkFor3D.Based_Operations;
using SVGImage.SVG.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DObjects
{
    public class Sphere3D : ModelVisual3D, BasedOperation
    {
        #region Variables
        private Point3DCollection vertices;
        private Vector3DCollection normals;
        private Int32Collection indices;

        #endregion
        public Sphere3D()
        {  
            readSphere("D:/GitHub/Platforma-Dezvoltare-Jocuri-Retro-2D-sI-3D/" +
                "FramworlFor3D/3D Models/sphere.obj");
        }
        private void readSphere(string filePath)
        {
            
            DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            ModelVisual3D lightOfSphere = new ModelVisual3D();
           
            lightOfSphere.Content = light;
            if (!File.Exists(filePath)) return;
            vertices = new Point3DCollection();
            normals = new Vector3DCollection();
            indices = new Int32Collection();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts[0] == "v")
                    {
                        float x = float.Parse(parts[1]);
                        float y= float.Parse(parts[2]);
                        float z = float.Parse(parts[3]);
                        Point3D newPoint= new Point3D(x, y, z);
                        vertices.Add(newPoint);
                    }
                    if (parts[0]=="vn")
                    {
                        double x = double.Parse(parts[1]);
                        double y = double.Parse(parts[2]);
                        double z = double.Parse(parts[3]);
                        Vector3D newNormalsVector=new Vector3D(x, y, z);
                        normals.Add(newNormalsVector);
                    }
                    if (parts[0]=="f")
                    {
                        string[] index1 = parts[1].Split('/');
                        string[] index2 = parts[2].Split('/');
                        string[] index3 = parts[3].Split('/');
                    
                        int index1INT = int.Parse(index1[0])-1;
                        int index2INT = int.Parse(index2[0])-1;
                        int index3INT = int.Parse(index3[0])-1;
                        indices.Add(index1INT);

                        indices.Add(index2INT);

                        indices.Add(index3INT);




                    }
                }
            }
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions= vertices;
            mesh.Normals= normals;
            mesh.TriangleIndices= indices;
            double scale = 0.03;
            for (int i = 0; i < mesh.Positions.Count; i++)
            {
                Point3D originalPosition = mesh.Positions[i];
                Point3D scaledPosition = new Point3D(originalPosition.X * scale, originalPosition.Y * scale, originalPosition.Z * scale);
                mesh.Positions[i] = scaledPosition;
            }
            DiffuseMaterial material= new DiffuseMaterial();
            material.Brush = Brushes.LightGray;
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup lightAndGeometry = new Model3DGroup();
            lightAndGeometry.Children.Add(lightOfSphere.Content);
            lightAndGeometry.Children.Add(model);
            ModelVisual3D sphere=new ModelVisual3D();
            sphere.Content = lightAndGeometry;
            Content = sphere.Content;
            
        }
            public void Rotate(double angle,Vector3D axis)
            {
            AxisAngleRotation3D axisS = new AxisAngleRotation3D(axis,angle);
            RotateTransform3D rotate= new RotateTransform3D(axisS);
            Transform=rotate;
            }

            public void Scale()
            {
                throw new NotImplementedException();
            }

            public void Translate()
            {
                throw new NotImplementedException();
            }
        }
    }
