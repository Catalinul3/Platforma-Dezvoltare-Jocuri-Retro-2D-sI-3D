using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.Models
{
    public class Scenes : ModelVisual3D
    {
        ModelVisual3D sky { get; set; }
        List<ModelVisual3D> grid { get; set; }
        Model3DGroup axis { get; set; }
        public List<ModelVisual3D> getGrid()
        {
            return grid;
        }
        public Scenes()
        {
            sky = new ModelVisual3D();
            grid = createGrid();
            axis = new Model3DGroup();
        }
        public ModelVisual3D getCenterOfGrid(List<ModelVisual3D>grid)
        {
            return grid[grid.Count / 2];
        }
        public List<ModelVisual3D> createGrid()
        {
            //DirectionalLight light = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            List<ModelVisual3D> grid = new List<ModelVisual3D>();
            ModelVisual3D floor=new ModelVisual3D();
            ModelVisual3D lightVisual = new ModelVisual3D();
            Model3DGroup floorGroup = new Model3DGroup();
          //  lightVisual.Content = light;
            double cellSize = 0.3;
            double delim = 0.09;
 




            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    double xPos = x * (cellSize - delim);
                    double yPos = y * (cellSize - delim);

                    MeshGeometry3D mesh = new MeshGeometry3D();
                    mesh.Positions.Add(new Point3D(xPos + cellSize, yPos + cellSize, 0));
                    mesh.Positions.Add(new Point3D(xPos, yPos + cellSize, 0));
                    mesh.Positions.Add(new Point3D(xPos, yPos, 0));
                    mesh.Positions.Add(new Point3D(xPos + cellSize, yPos, 0));

                    mesh.TextureCoordinates.Add(new Point(1, 1));
                    mesh.TextureCoordinates.Add(new Point(0, 1));
                    mesh.TextureCoordinates.Add(new Point(0, 0));
                    mesh.TextureCoordinates.Add(new Point(1, 0));

                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(1);
                    mesh.TriangleIndices.Add(2);
                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(2);
                    mesh.TriangleIndices.Add(3);

                    GeometryModel3D cell = new GeometryModel3D();
                    cell.Geometry = mesh;
                    ImageBrush image = new ImageBrush();
                    image.ImageSource = new BitmapImage(new Uri("D:\\GitHub\\Platforma-Dezvoltare-Jocuri-Retro-2D-sI-3D\\FramworlFor3D\\Resources\\cell.png"));
                    cell.Material = new DiffuseMaterial(image);



                    Model3DGroup modelGroup = new Model3DGroup();
                    modelGroup.Children.Add(cell);

                    ModelVisual3D cellVisual = new ModelVisual3D();
                    cellVisual.Content = modelGroup;


                    grid.Add(cellVisual);

                }
            }


            return grid;

        }
       
    }
}
