using Eco.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using FramworkFor3D.Based_Operations;
using System.Windows;
using SharpDX.Direct3D9;

namespace FramworkFor3D._3DObjects
{
    public class Irregular3DObject : ModelVisual3D, BasedOperation
    {
        #region Variables
        private Point3DCollection vertices;

        private Vector3DCollection normals;
        private Int32Collection indices;
        private PointCollection texture;


        #endregion
        public Irregular3DObject(string fileName)
        {
            read3dObject(fileName);
        }



        private void read3dObject(string filePath)
        {

            DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            ModelVisual3D lightOfIrregular = new ModelVisual3D();

            lightOfIrregular.Content = light;
            if (!File.Exists(filePath)) return;
            vertices = new Point3DCollection();
            normals = new Vector3DCollection();
            indices = new Int32Collection();
            texture = new PointCollection();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts[0] == "v" && parts[1] == "")
                    {
                        float x = float.Parse(parts[2]);
                        float y = float.Parse(parts[3]);
                        float z = float.Parse(parts[4]);
                        Point3D newPoint = new Point3D(x, y, z);
                        vertices.Add(newPoint);

                    }
                    if (parts[0] == "v" && parts[1] != "")
                    {
                        float x = float.Parse(parts[1]);
                        float y = float.Parse(parts[2]);
                        float z = float.Parse(parts[3]);
                        Point3D newPoint = new Point3D(x, y, z);
                        vertices.Add(newPoint);

                    }
                    if (parts[0] == "vt" && parts[1] == "")
                    {
                        double x = double.Parse(parts[2]);
                        double y = double.Parse(parts[3]);

                        Point newTextureCoordinates = new Point(x, y);
                        texture.Add(newTextureCoordinates);
                    }
                    if (parts[0] == "vt" && parts[1] != "")
                    {
                        double x = double.Parse(parts[1]);
                        double y = double.Parse(parts[2]);

                        Point newTextureCoordinates = new Point(x, y);
                        texture.Add(newTextureCoordinates);
                    }
                    if (parts[0] == "vn" && parts[1] == "")
                    {
                        double x = double.Parse(parts[2]);
                        double y = double.Parse(parts[3]);
                        double z = double.Parse(parts[4]);
                        Vector3D newNormalsVector = new Vector3D(x, y, z);
                        normals.Add(newNormalsVector);
                    }
                    if (parts[0] == "vn" && parts[1] != "")
                    {
                        double x = double.Parse(parts[1]);
                        double y = double.Parse(parts[2]);
                        double z = double.Parse(parts[3]);
                        Vector3D newNormalsVector = new Vector3D(x, y, z);
                        normals.Add(newNormalsVector);
                    }
                    if (parts.Length == 6)

                    {
                        if (parts[0] == "f" && parts[1] != "")
                        {
                            string[] index1 = parts[1].Split('/');
                            string[] index2 = parts[2].Split('/');
                            string[] index3 = parts[3].Split('/');
                            string[] index4 = parts[4].Split('/');

                            int index1INT = int.Parse(index1[0]) - 1;
                            int index2INT = int.Parse(index2[0]) - 1;
                            int index3INT = int.Parse(index3[0]) - 1;
                            int index4INT = int.Parse(index4[0]) - 1;
                            indices.Add(index1INT);

                            indices.Add(index2INT);

                            indices.Add(index3INT);
                            indices.Add(index1INT);
                            indices.Add(index3INT);

                            indices.Add(index4INT);
                        }
                        if (parts[0] == "f" && parts[1] == "")
                        {
                            string[] index1 = parts[2].Split('/');
                            string[] index2 = parts[3].Split('/');
                            string[] index3 = parts[4].Split('/');
                            string[] index4 = parts[5].Split('/');

                            int index1INT = int.Parse(index1[0]) - 1;
                            int index2INT = int.Parse(index2[0]) - 1;
                            int index3INT = int.Parse(index3[0]) - 1;
                            int index4INT = int.Parse(index4[0]) - 1;
                            indices.Add(index1INT);

                            indices.Add(index2INT);

                            indices.Add(index3INT);
                            indices.Add(index1INT);
                            indices.Add(index3INT);

                            indices.Add(index4INT);
                        }
                    }
                    else
                    {
                        if (parts.Length - 1 == 3)
                        {
                            if (parts[0] == "f" && parts[1] != "")
                            {
                                string[] index1 = parts[1].Split('/');
                                string[] index2 = parts[2].Split('/');
                                string[] index3 = parts[3].Split('/');

                                int index1INT = int.Parse(index1[0]) - 1;
                                int index2INT = int.Parse(index2[0]) - 1;
                                int index3INT = int.Parse(index3[0]) - 1;
                                indices.Add(index1INT);

                                indices.Add(index2INT);

                                indices.Add(index3INT);




                            }
                            if (parts[0] == "f" && parts[1] == "")
                            {
                                string[] index1 = parts[2].Split('/');
                                string[] index2 = parts[3].Split('/');
                                string[] index3 = parts[4].Split('/');

                                int index1INT = int.Parse(index1[0]) - 1;
                                int index2INT = int.Parse(index2[0]) - 1;
                                int index3INT = int.Parse(index3[0]) - 1;
                                indices.Add(index1INT);

                                indices.Add(index2INT);

                                indices.Add(index3INT);

                            }
                        }
                        else
                        {
                            if (parts[0] == "f" && parts[1] != "")
                            {
                                string[] index1 = parts[1].Split('/');
                                string[] index2 = parts[2].Split('/');
                                string[] index3 = parts[3].Split('/');
                                string[] index4 = parts[4].Split('/');
                                int index1INT = int.Parse(index1[0]) - 1;
                                int index2INT = int.Parse(index2[0]) - 1;
                                int index3INT = int.Parse(index3[0]) - 1;
                                int index4INT = int.Parse(index4[0]) - 1;
                                indices.Add(index1INT);

                                indices.Add(index2INT);

                                indices.Add(index3INT);
                                indices.Add(index1INT);
                                indices.Add(index3INT);
                                indices.Add(index4INT);




                            }
                        

                        }
                    }

                }
            }
            MeshGeometry3D mesh = new MeshGeometry3D();

            mesh.Normals = normals;
            mesh.TriangleIndices = indices;
            mesh.TextureCoordinates = texture;
            double scale = 0.03;
            for (int i = 0; i < vertices.Count; i++)
            {
                Point3D originalPosition = vertices[i];
                Point3D scaledPosition = new Point3D(originalPosition.X * scale, originalPosition.Y * scale, originalPosition.Z * scale);
             
                vertices[i] = scaledPosition;
            }
            mesh.Positions = vertices;


            DiffuseMaterial material = new DiffuseMaterial();
            material.Brush = Brushes.Red;
          
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup lightAndGeometry = new Model3DGroup();
            lightAndGeometry.Children.Add(lightOfIrregular.Content);
            lightAndGeometry.Children.Add(model);
            ModelVisual3D irregular = new ModelVisual3D();
           
        

        irregular.Content = lightAndGeometry;
          
         
            Content = irregular.Content;  

        }
        public void Rotate(double angle)
        {
            throw new NotImplementedException();
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
