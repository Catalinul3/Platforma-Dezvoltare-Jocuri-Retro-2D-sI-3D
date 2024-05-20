using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using FramworkFor3D._3DObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System.Xaml;
using Matrix = BulletSharp.Math.Matrix;

namespace FramworkFor3D._3DPhysics
{
    public class RigidBodyPhysics
    {


        private float mass;
        private RigidBody rigidBody;
        private BoxShape colShape;
        private Vector3 inertia;
        private RigidBodyConstructionInfo info;


        public RigidBodyPhysics()
        {
            mass = 1.0f;
            colShape = new BoxShape(1);
            inertia = colShape.CalculateLocalInertia(mass);
            info = new RigidBodyConstructionInfo(mass, null, colShape, inertia);



        }
       public Matrix GetTransform()
        {
            return rigidBody.WorldTransform;
        }
        public RigidBody Fall()
        {
           

            var startPos = new Vector3(0, 20, 0);
            var position = startPos + new Vector3(0, 0, 10);
            info.MotionState = new DefaultMotionState(BulletSharp.Math.Matrix.Translation(position));
            var body = new RigidBody(info);
            return body;

            
        }
        public RigidBodyPhysics(RigidBodyConstructionInfo info)
        {
            rigidBody = new RigidBody(info);
        }




    }
}
