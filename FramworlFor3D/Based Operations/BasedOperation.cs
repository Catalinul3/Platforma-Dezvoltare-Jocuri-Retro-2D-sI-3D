using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D.Based_Operations
{
    public interface BasedOperation
    {
        
        void Rotate(double angle);
        void Translate();
        void Scale();
    }
}
