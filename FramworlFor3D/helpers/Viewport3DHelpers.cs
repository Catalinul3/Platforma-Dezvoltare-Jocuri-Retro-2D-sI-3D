using _3DTools;
using Eco.UmlRt;
using FramworkFor3D._3DObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace FramworkFor3D.helpers
{
    public class Viewport3DHelpers
    {
        GeneralTransform3DTo2D transform;
        Point3DCollection vertices;
        Int32Collection indices;
        private Point3DCollection getVertices(ModelVisual3D visual)
        {
            if (visual.Content is Model3DGroup model3Dgroup)
            {
                foreach (var model in model3Dgroup.Children)
                {
                    if (model is GeometryModel3D geometryModel)
                    {
                       if(geometryModel.Geometry is MeshGeometry3D meshGeometry)
                        {
                            vertices = meshGeometry.Positions;
                        }

                    }
                }
            }
            return vertices;
        }
  private Int32Collection getIndices(ModelVisual3D visual)
        {
            if (visual.Content is Model3DGroup model3Dgroup)
            {
                foreach (var model in model3Dgroup.Children)
                {
                    if (model is GeometryModel3D geometryModel)
                    {
                        if (geometryModel.Geometry is MeshGeometry3D meshGeometry)
                        {
                            indices = meshGeometry.TriangleIndices;
                        }

                    }
                }
            }
            return indices;
        }

        public void BuildSkeleton(ModelVisual3D visual)
        {
            vertices = getVertices(visual);
            indices = getIndices(visual);
            if (vertices != null && indices != null)
            {
                for (int i = 0; i < indices.Count; i += 3)
                {
                    Point3D p0 = vertices[indices[i]];
                    Point3D p1 = vertices[indices[i + 1]];
                    Point3D p2 = vertices[indices[i + 2]];
                    Line line1 = new Line();
                    line1.X1 = p0.X*100;
                    line1.Y1 = p0.Y*100;
                    line1.X2 = p1.X*100;
                    line1.Y2 = p1.Y * 100;
                    line1.Stroke = Brushes.Black;
                    line1.StrokeThickness = 10;
                    Line line2 = new Line();
                    line2.X1 = p1.X * 100;
                    line2.Y1 = p1.Y * 100;
                    line2.X2 = p2.X * 100;
                    line2.Y2 = p2.Y * 100;
                    line2.Stroke = Brushes.Black;
                    line2.StrokeThickness = 10;
                    Line line3 = new Line();
                    line3.X1 = p2.X * 100;
                    line3.Y1 = p2.Y * 100;
                    line3.X2 = p0.X * 100;
                    line3.Y2 = p0.Y * 100;
                    line3.Stroke = Brushes.Black;
                    line3.StrokeThickness = 10;
                    if (visual is Cube3D)
                    {
                        Cube3D cube = visual as Cube3D;
                        cube.objSkeleton.Add(line1);
                        cube.objSkeleton.Add(line2);
                        cube.objSkeleton.Add(line3);
                    }
                
             

                    //else if (visual is Sphere3D)
                    //{
                    //    Sphere3D sphere = visual as Sphere3D;
                    //    sphere.objSkeleton.Children.Add(line1);
                    //    sphere.objSkeleton.Children.Add(line2);
                    //    sphere.objSkeleton.Children.Add(line3);
                    //}
                    //else
                    //{
                    //    Irregular3DObject obj = visual as Irregular3DObject;
                    //    obj.objSkeleton.Children.Add(line1);
                    //    obj.objSkeleton.Children.Add(line2);
                    //    obj.objSkeleton.Children.Add(line3);
                    //}
                }
            }
        }

    }
}
