using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D.Animations
{
    public class AnimationClip
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        public List<AnimationFrame> Frames { get; set; }
        public AnimationClip()
        {
            Frames = new List<AnimationFrame>();
        }
    }
}
