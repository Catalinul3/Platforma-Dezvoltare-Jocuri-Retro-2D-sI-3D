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
        void Translate();
        void Scale();
    }
}
