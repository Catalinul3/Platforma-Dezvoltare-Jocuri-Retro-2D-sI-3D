using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.Based_Operations
{
    public interface BasedOperation
    {
        
        void Rotate(double angle, Vector3D AXIS);
        void Translate(Vector3D axis);
        void Scale(Vector3D axis, double scaleFactor);
    }
}
