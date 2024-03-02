using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.Models
{
    public class Scenes : ModelVisual3D
    {
        ModelVisual3D sky { get; set; }
        List<ModelVisual3D> grid { get; set; }
        Model3DGroup axis { get; set; }

        public Scenes()
        {
            sky = new ModelVisual3D();
            grid = createGrid();
            axis = new Model3DGroup();
        }
        public List<ModelVisual3D> createGrid()
        {

            List<ModelVisual3D> grid = new List<ModelVisual3D>();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                { MeshGeometry3D mesh=new MeshGeometry3D();
                    mesh.Positions.Add(new Point3D(x+1, y+1, 0));
                    mesh.Positions.Add(new Point3D(x-1, y+1, 0));
                    mesh.Positions.Add(new Point3D(x-1, y-1, 0));
                    mesh.Positions.Add(new Point3D(x+1, y-1, 0));

                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(1);
                    mesh.TriangleIndices.Add(2);
                    mesh.TriangleIndices.Add(0);
                    mesh.TriangleIndices.Add(2);
                    mesh.TriangleIndices.Add(3);

                    GeometryModel3D cell = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Gray));
                    ModelVisual3D cellVisual = new ModelVisual3D();
                    cellVisual.Content = cell;
                    grid.Add(cellVisual);
                }
            }


            return grid;

        }
    }
}
