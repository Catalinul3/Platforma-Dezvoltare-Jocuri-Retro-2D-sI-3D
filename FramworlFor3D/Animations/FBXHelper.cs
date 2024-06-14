using Assimp;
using FramworkFor3D._3DObjects;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.Animations
{
    public static class FBXHelper
    {
        public static Scene  readFile(string path)
        {
           using (var importer=new AssimpContext())
            {
                Scene scene = importer.ImportFile(path, PostProcessSteps.None);
                return scene;
            }
            return null;
        }
        public static List<Irregular3DObject> buildModel(Scene file)
        {
            List<Irregular3DObject> irregular = new List<Irregular3DObject>();
            Point3DCollection vertices = new Point3DCollection();
            PointCollection tecsture = new PointCollection();
            Int32Collection indices = new Int32Collection();
            foreach(var mesh in file.Meshes)
            {   ModelVisual3D model=new ModelVisual3D();
                vertices = convertToPoint3D(mesh.Vertices);
                indices = convertToInt32Collection(mesh.Faces);
                
                MeshGeometry3D geometry=new MeshGeometry3D();
                geometry.Positions = vertices;
                geometry.TriangleIndices = indices;

                GeometryModel3D modelGeometry = new GeometryModel3D(geometry, new DiffuseMaterial(Brushes.Gray));
                model.Content= modelGeometry;
                Irregular3DObject newObject=new Irregular3DObject(model);
                
                irregular.Add(newObject);
            }
            return irregular;
        }
        private static Point3DCollection convertToPoint3D(List<Assimp.Vector3D> vertices)
        {
            Point3DCollection point3DVertices = new Point3DCollection();
            foreach(var v in vertices)
            {
                point3DVertices.Add(new Point3D(v.X,v.Y,v.Z));
            }
            return point3DVertices;

        }
        private static Int32Collection convertToInt32Collection(List<Face>triangleIndices)
        {
            Int32Collection indices = new Int32Collection();
            foreach(var face in triangleIndices)
            {
                foreach(var indice in face.Indices)
                {
                    indices.Add(indice);
                }
            }
            return indices;
        }
    }
}
