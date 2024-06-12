using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.Animations
{
   public class AnimationFrame
    {
        public double Time;
        public Dictionary<string, Transform3D> transformation;
        public AnimationFrame()
        {
            Time = 0.0d;
            transformation= new Dictionary<string, Transform3D>();
        }
    }
}
