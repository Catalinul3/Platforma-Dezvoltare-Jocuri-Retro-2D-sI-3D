using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D.helpers
{
    public static class RetroTime
    {
        public static Stopwatch DeltaTime
        {
            get
            {
                return new Stopwatch();
            }
        }
        
    }
}
