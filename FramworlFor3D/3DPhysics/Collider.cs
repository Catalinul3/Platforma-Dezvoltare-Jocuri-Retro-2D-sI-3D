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
    public static class Collider
    {
       public static bool IsColliding(ModelVisual3D obj1,ModelVisual3D obj2)
        {  Rect3D primaryBounds= obj1.Content.Bounds;
       
           Rect3D objectInCollision= obj2.Content.Bounds;

         Rect3D intersectionRectangle=Rect3D.Intersect(primaryBounds, objectInCollision);
            if (intersectionRectangle.IsEmpty)
            {
                return false;
            }
            else
            {
                return true;
            }
         
            
        }

    }
}
