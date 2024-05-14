using FramworkFor3D._3DObjects;
using SVGImage.SVG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace FramworkFor3D._3DPhysics
{
    public class Collider
    {
        private Cube3D cube;
        private Transform3D transform;
        private MeshGeometry3D geometry;
        private Viewport3D scene;
        Rectangle marg;
        public Collider(Cube3D cube, Viewport3D scene)
        {
            this.cube= cube;
            this.scene = scene;
            
        }
       public Thickness ShowLines(Viewport3D scene)
        {

            if (cube.Content is Model3DGroup modelGroup)
            {
                foreach (var childModel in modelGroup.Children)
                {
                    if (childModel is GeometryModel3D geometryModel)
                    {
                      
                       transform = geometryModel.Transform;
                         geometry = (MeshGeometry3D)geometryModel.Geometry;

                        
                    }
                }
            }
            Rect allMargins = Rect.Empty;
            if(transform!=null)
            {
                for(int i=0;i<geometry.TriangleIndices.Count;i++)
                {
                    Polyline highlight = new Polyline
                    {
                        Stroke = Brushes.Orange,
                        StrokeThickness = 0.25,
                    };
                 for(int j=0;j<3;j++)
                    {
                        Point3D point = geometry.Positions[geometry.TriangleIndices[i++]];
                        Point3D transformedPoint = transform.Transform(point);
                        Point point2D = new Point(transformedPoint.X, transformedPoint.Y);
                        highlight.Points.Add(point2D);
                    }

                   
                    foreach (Point point in highlight.Points)
                    {
                        allMargins.Union(point);
                    }
                }
            }
            marg= new Rectangle();
            marg.Width = allMargins.Width*100;
            marg.Height = allMargins.Height*100;
            marg.Margin = new Thickness(0,0,allMargins.Right*100,allMargins.Bottom * 100);
            return marg.Margin;

        }
        public bool checkCollisionOnLeft(Cube3D cube,ModelVisual3D other)
        {
            bool collision = false;
           
            return false;
        }

    }
}
