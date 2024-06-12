using Assimp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D.Animations
{
    public class KeyFrame
    {
        private string _nodeName;
        public string NodeName { get { return _nodeName; } set { _nodeName = value; } }
        private List<Vector3D> _position;
        private List<Quaternion> _rotation;
        private List<Vector3D> _scale;
        public List<Vector3D> Position { get { return _position; } set { _position = value; } }
        public List<Quaternion> Rotation { get { return _rotation; }   set { _rotation = value; } }
        public List<Vector3D> Scale { get { return _scale; } set { _scale = value; } }
        
        public KeyFrame()
        {
            _nodeName = "Default";
            _position = new List<Vector3D>();
            _rotation = new List<Quaternion>();
            _scale = new List<Vector3D>();

        }
    }
}
