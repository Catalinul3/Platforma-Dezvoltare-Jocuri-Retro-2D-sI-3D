using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DPhysics
{
    public static class ObjectCollider
    {
       public static Rect3D objBounds;
        public static Rect3D getCurrentObjectCollider(ModelVisual3D obj)
        {
            return obj.Content.Bounds;
        }
        public static Rect3D modifyObjectBounds(ModelVisual3D obj,float radius)
        {
            objBounds = obj.Content.Bounds;
            objBounds.X -= radius/2;
            objBounds.Y -= radius/2;
            objBounds.Z -= radius / 2;
            objBounds.SizeX += radius;
            objBounds.SizeY += radius;
            objBounds.SizeZ += radius;
            return objBounds;
        }

    }
}
