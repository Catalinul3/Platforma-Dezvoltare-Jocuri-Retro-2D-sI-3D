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
        private double _duration;
        private List<KeyFrame> _keyFrames;
        public List<KeyFrame> KeyFrames { get { return _keyFrames; } set { _keyFrames = value; } }
        public double duration { get { return _duration; } set { _duration = value; } }
        public AnimationClip()
        {
            _name = "Default";
            _duration = 0;
            _keyFrames= new List<KeyFrame>();
        }
    }
}
