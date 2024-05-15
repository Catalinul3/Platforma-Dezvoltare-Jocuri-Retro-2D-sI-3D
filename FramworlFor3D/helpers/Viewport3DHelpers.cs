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
                        if (geometryModel.Geometry is MeshGeometry3D meshGeometry)
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





    }
}
