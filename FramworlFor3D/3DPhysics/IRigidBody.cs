using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DPhysics
{
   public interface IRigidBody
    {
        void ApplyForce(Vector3D force);
    }
}
