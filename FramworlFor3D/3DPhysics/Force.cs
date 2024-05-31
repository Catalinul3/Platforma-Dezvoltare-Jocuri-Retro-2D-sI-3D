using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DPhysics
{
    public class Force
    {
        float mass;
        float acceleration;
        float forceOfObject;
        public Force()
        {
            this.mass = 0;
            this.acceleration = 0;
            this.forceOfObject = 0;
        }
        public Force(float mass, float acceleration)
        {
            this.mass = mass;
            this.acceleration = acceleration;
            this.forceOfObject = getForce();
        }
        public float getForce()
        {
            return mass * acceleration;
        }
        public void setForce(float force)
        {
            this.forceOfObject = force;
        }
        
    }
}