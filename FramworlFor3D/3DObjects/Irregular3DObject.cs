
using System.IO;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using FramworkFor3D.Based_Operations;
using System.Windows;
using FramworkFor3D.helpers;
using System;

using System.Windows.Media.Imaging;
using System.Collections.Generic;


namespace FramworkFor3D._3DObjects
{
    public class Irregular3DObject : ModelVisual3D, BasedOperation
    {
        #region Variables
        private Point3DCollection vertices;

        private Vector3DCollection normals;
        private Int32Collection indices;
        private PointCollection texture;
        private PointCollection allTexture;
        private MaterialGroup defaultMaterial;
        private const ObjectType type = ObjectType.IRREGULAR;
        private List<ModelVisual3D> models;

        #endregion
        public Irregular3DObject()
        {

        }
        public Irregular3DObject(string fileName)
        {
            read3dObject(fileName);
        }
        public Irregular3DObject(ModelVisual3D obj)
        {
            this.Content = obj.Content;
        }


        #region Parse Data
        private void read3dObject(string filePath)
        {

            DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            ModelVisual3D lightOfIrregular = new ModelVisual3D();
            bool havemtl = false;
            lightOfIrregular.Content = light;
            if (!File.Exists(filePath)) return;
            vertices = new Point3DCollection();
            normals = new Vector3DCollection();
            indices = new Int32Collection();
            texture = new PointCollection();

            Dictionary<string, ModelVisual3D> partsOfModel = new Dictionary<string, ModelVisual3D>();
            ModelVisual3D part = new ModelVisual3D();
            models = new List<ModelVisual3D>();
            string nameOfPart = "";

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {


                    string[] parts = line.Split(' ');
                    if (parts[0] == "mtllib")
                    {
                        string matPath = Path.GetFullPath(filePath);
                        matPath = matPath.Replace(Path.GetFileName(filePath), parts[1]);

                        if (matPath != null)
                        {
                            defaultMaterial = readMtlFile(matPath);
                        }
                        havemtl = true;
                    }
                    //if (parts[0] == "o")
                    //{
                    //    nameOfPart = parts[1];
                    //    vertices = new Point3DCollection();
                    //    normals = new Vector3DCollection();
                    //    indices = new Int32Collection();
                    //    texture = new PointCollection();
                    //    defaultMaterial = new MaterialGroup();
                    //    allTexture = new PointCollection();

                    //    part = new ModelVisual3D();



                    //}
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

                        Point newTextureCoordinates = new Point(x, 1 - y);
                        texture.Add(newTextureCoordinates);
                    }
                    if (parts[0] == "vt" && parts[1] != "")
                    {
                        double x = double.Parse(parts[1]);
                        double y = double.Parse(parts[2]);

                        Point newTextureCoordinates = new Point(x, 1 - y);
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
                  //  if (parts[0] == "usemtl")
                   // {
                        //MeshGeometry3D meshPart = new MeshGeometry3D();


                        //allTexture = texture;
                        //for (int i = texture.Count - 1; i >= 0; i--)
                        //{
                        //    allTexture.Add(texture[i]);
                        //}
                        //meshPart.TextureCoordinates = allTexture;
                        ////double scale = 0.05;
                        ////for (int i = 0; i < vertices.Count; i++)
                        ////{
                        ////    Point3D originalPosition = vertices[i];
                        ////    Point3D scaledPosition = new Point3D(originalPosition.X * scale, originalPosition.Y * scale, originalPosition.Z * scale);

                        ////    vertices[i] = scaledPosition;
                        ////}
                        //meshPart.Positions = vertices;
                        //meshPart.TriangleIndices = indices;

                        //DiffuseMaterial materialPart = new DiffuseMaterial();
                        //GeometryModel3D modelPart = new GeometryModel3D();
                        //materialPart.Brush = Brushes.LightGray;
                        //modelPart = new GeometryModel3D(meshPart, materialPart);
                        //Model3DGroup lightAndGeometry = new Model3DGroup();

                        //lightAndGeometry.Children.Add(modelPart);
                        //lightAndGeometry.Children.Add(lightOfIrregular.Content);
                        //part.Content = lightAndGeometry;
                        //if (partsOfModel.ContainsKey(nameOfPart))
                        //{
                        //    part = partsOfModel[nameOfPart];
                            
                        //}
                        //else
                        //{
                        //    partsOfModel.Add(nameOfPart, part);

                        //}
                   // }
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
                        if (parts.Length == 5)
                        {
                            if (parts[0] == "f" && parts[1] != "" && (parts[4] == null || parts[4] == " "))
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

                            if (parts[0] == "f" && parts[1] != "" && parts[4] != "")
                            {
                                string[] index1 = parts[1].Split('/');
                                string[] index2 = parts[2].Split('/');
                                string[] index3 = parts[3].Split('/');
                                string[] index4 = parts[4].Split('/');


                                //cazul in care avem "// " in f 


                                int index1INT = int.Parse(index1[0]) - 1;
                                int index2INT = int.Parse(index2[0]) - 1;
                                int index3INT = int.Parse(index3[0]) - 1;
                                int index4INT = int.Parse(index4[0]) - 1;
                                if (index1.Length == 1 && index2.Length == 1 && index3.Length == 1 && index4.Length == 1)
                                {
                                    indices.Add(index1INT);
                                    indices.Add(index2INT);
                                    indices.Add(index3INT);

                                    indices.Add(index1INT);
                                    indices.Add(index3INT);
                                    indices.Add(index4INT);


                                }
                                else
                                {
                                    if (index1[1] == "" || index2[1] == "" || index3[1] == "" || index4[1] == "")
                                    {
                                        index1[1] = "0";
                                        index2[1] = "0";
                                        index3[1] = "0";
                                        index4[1] = "0";
                                    }
                                    else
                                    {


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
                        else
                        {
                            if (parts.Length == 4)
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
                            }
                        }
                    }
                }

            }
            MeshGeometry3D meshPart = new MeshGeometry3D();


            allTexture = texture;
            for (int i = texture.Count - 1; i >= 0; i--)
            {
                allTexture.Add(texture[i]);
            }
            meshPart.TextureCoordinates = allTexture;
            double scale = 0.05;
            for (int i = 0; i < vertices.Count; i++)
            {
                Point3D originalPosition = vertices[i];
                Point3D scaledPosition = new Point3D(originalPosition.X * scale, originalPosition.Y * scale, originalPosition.Z * scale);

                vertices[i] = scaledPosition;
            }
            meshPart.Positions = vertices;
            meshPart.TriangleIndices = indices;

            DiffuseMaterial materialPart = new DiffuseMaterial();
            GeometryModel3D modelPart = new GeometryModel3D();
            materialPart.Brush = Brushes.LightGray;
            modelPart = new GeometryModel3D(meshPart, materialPart);
            Model3DGroup lightAndGeometry = new Model3DGroup();

            lightAndGeometry.Children.Add(modelPart);
            lightAndGeometry.Children.Add(lightOfIrregular.Content);
            part.Content = lightAndGeometry;
            Content= part.Content;
            //Model3DGroup allObject = new Model3DGroup();
            //foreach (var objects in partsOfModel.Values)
            //{
            //    models.Add(objects);

            //}
            //foreach (var model in models)
            //{
            //    allObject.Children.Add(model.Content);
            //}
            //ModelVisual3D modelVisual = new ModelVisual3D();
            //modelVisual = part;
            //Content = modelVisual.Content;


            //MeshGeometry3D mesh = new MeshGeometry3D();

            //// mesh.Normals = normals;
            //mesh.TriangleIndices = indices;
            //allTexture = texture;
            //for (int i = texture.Count - 1; i >= 0; i--)
            //{
            //    allTexture.Add(texture[i]);
            //}
            //mesh.TextureCoordinates = allTexture;
            //double scale = 0.05;
            //for (int i = 0; i < vertices.Count; i++)
            //{
            //    Point3D originalPosition = vertices[i];
            //    Point3D scaledPosition = new Point3D(originalPosition.X * scale, originalPosition.Y * scale, originalPosition.Z * scale);

            //    vertices[i] = scaledPosition;
            //}
            //mesh.Positions = vertices;

            //DiffuseMaterial material = new DiffuseMaterial();
            //GeometryModel3D model = new GeometryModel3D();
            //material.Brush = Brushes.LightGray;
            //if (havemtl)

            //{ model = new GeometryModel3D(mesh, defaultMaterial); }
            //else
            //{
            //    model = new GeometryModel3D(mesh, material);
            //}
            //Model3DGroup lightAndGeometry = new Model3DGroup();

            //lightAndGeometry.Children.Add(model);
            //lightAndGeometry.Children.Add(lightOfIrregular.Content);
            //ModelVisual3D irregular= new ModelVisual3D();
            //irregular.Content =lightAndGeometry ;
            //Content = irregular.Content;

            MessageBox.Show("Object have " + partsOfModel.Count + " components");

        }
        public MaterialGroup readMtlFile(string fileName)
        {
            if (!File.Exists(fileName)) return null;
            List<MaterialGroup> materials = new List<MaterialGroup>();
            MaterialGroup currentMaterial = new MaterialGroup();
            string currentMaterialName = "";
            System.Windows.Media.Color ka = Colors.White, kd = Colors.White, ks = Colors.White;
            double ns = 0.0, ni = 0.0, d = 0.0, illum = 0.0;
            string map_Kd = "", map_Ka = "";
            string directory = Path.GetDirectoryName(fileName);
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    DiffuseMaterial mat = new DiffuseMaterial();
                    if (parts[0] == "newmtl")
                    {
                        currentMaterialName = parts[1];


                    }
                    if (parts[0] == "Ns")
                    {
                        ns = double.Parse(parts[1]);
                    }
                    if (parts[0] == "Ka")
                    {
                        byte red = (byte)((float.Parse(parts[1])) * 255);
                        byte green = (byte)((float.Parse(parts[2])) * 255);
                        byte blue = (byte)((float.Parse(parts[3])) * 255);
                        ka = System.Windows.Media.Color.FromRgb(red, green, blue);
                        currentMaterial.Children.Add(new DiffuseMaterial(new SolidColorBrush(ka)));

                    }
                    if (parts[0] == "Kd")
                    {
                        byte red = (byte)((float.Parse(parts[1])) * 255);
                        byte green = (byte)((float.Parse(parts[2])) * 255);
                        byte blue = (byte)((float.Parse(parts[3])) * 255);
                        kd = System.Windows.Media.Color.FromRgb(red, green, blue);
                        currentMaterial.Children.Add(new DiffuseMaterial(new SolidColorBrush(kd)));
                    }
                    if (parts[0] == "Ks")
                    {
                        byte red = (byte)((float.Parse(parts[1])) * 255);
                        byte green = (byte)((float.Parse(parts[2])) * 255);
                        byte blue = (byte)((float.Parse(parts[3])) * 255);
                        ks = System.Windows.Media.Color.FromRgb(red, green, blue);
                        currentMaterial.Children.Add(new SpecularMaterial(new SolidColorBrush(ka), ns));
                    }
                    if (parts[0] == "Ni")
                    {
                        ni = double.Parse(parts[1]);
                    }
                    if (parts[0] == "d")
                    { d = double.Parse(parts[1]); }
                    if (parts[0] == "illum")
                    { illum = double.Parse(parts[1]); }
                    if (parts[0] == "map_Kd")
                    {
                        map_Kd = parts[1];





                    }
                    if (parts[0] == "map_Ka")
                    {
                        map_Ka = parts[1];





                    }
                }
            }
            return currentMaterial;

        }
        private ImageBrush LoadTexture(string textureFilePath)
        {
            if (!File.Exists(textureFilePath))
            {
                // Tratarea erorii dacă fișierul texturii nu există
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage(new Uri(textureFilePath));
            ImageBrush imageBrush = new ImageBrush(bitmapImage);
            return imageBrush;
        }

        #endregion

        #region BasicTransformations
        public void Rotate(double angle, Vector3D axis)
        {//rotatie indiferent de axa 
            AxisAngleRotation3D axisS = new AxisAngleRotation3D(axis, angle);
            RotateTransform3D rotate = new RotateTransform3D(axisS);
            this.Transform = rotate;
        }

        public void Scale(Vector3D axis, double scaleFactor)
        {//scalare pe axa x cu factorul scaleFactor
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

        public void Translate(Vector3D axis)
        { //translatie pe axa x
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


    }
    #endregion

}
