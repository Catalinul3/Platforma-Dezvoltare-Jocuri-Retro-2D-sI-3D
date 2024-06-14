
using System.Windows.Media.Media3D;


namespace FramworkFor3D._3DPhysics
{
    public static class Collider
    {
       public static bool IsColliding(Rect3D bound1,Rect3D bound2)
        { 

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
        public static Rect3D UpdateColliderObject(Rect3D objectBound,TranslateTransform3D translate)
        {
            return new Rect3D
            {
                X = objectBound.X + translate.OffsetX,
                Y = objectBound.Y + translate.OffsetY,
                Z = objectBound.Z + translate.OffsetZ,
                SizeX = objectBound.SizeX,
                SizeY = objectBound.SizeY,
                SizeZ = objectBound.SizeZ,
            };
        }
        

    }
}
