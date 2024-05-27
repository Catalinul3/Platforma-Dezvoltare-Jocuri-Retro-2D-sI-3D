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
       public static bool IsColliding(Rect3D bound1,Rect3D bound2)
        { 
         // MessageBox.Show("Location object 1 = "+bound1.Location.ToString() +"\n Location object 2 = "+bound2.Location.ToString()
              //+"\n Size object 1 = "+bound1.Size.ToString()+"\n Size object 2 = "+bound2.Size.ToString());
          return bound1.IntersectsWith(bound2);
         
            
        }
        public static Rect3D UpdateBounds(ModelVisual3D obj,TranslateTransform3D translate)
        {
            return new Rect3D
            {
                X = obj.Content.Bounds.X + translate.OffsetX,
                Y = obj.Content.Bounds.Y + translate.OffsetY,
                Z = obj.Content.Bounds.Z + translate.OffsetZ,
                SizeX= obj.Content.Bounds.SizeX,
                SizeY= obj.Content.Bounds.SizeY,
                SizeZ = obj.Content.Bounds.SizeZ,
            };
        }

    }
}
