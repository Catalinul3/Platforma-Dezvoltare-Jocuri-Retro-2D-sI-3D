using BulletSharp;
using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DPhysics
{
    public class RigidBodyPhysics
    {
        private RigidBody body;
        private double force;
        private float mass;
        private CollisionShape shape;
        private Vector3D position;
        private Vector3D velocity;
        private const double acceleration=9.8;
        private const double density = 1000.00;
        private double weight;
        public RigidBodyPhysics()
        {
            shape = new BoxShape(1);
            mass = 1;
            position = new Vector3D(0, 0, 0);
            RigidBodyConstructionInfo bodyInfo = new RigidBodyConstructionInfo(mass, null, shape);
            body = new RigidBody(bodyInfo);
        }
       public RigidBodyPhysics(CollisionShape shape,float mass,Vector3D position)
        {
            this.shape = shape;
            this.mass = mass;
            this.position= position;

            RigidBodyConstructionInfo bodyInfo = new RigidBodyConstructionInfo(mass, null, shape);
            body = new RigidBody(bodyInfo);
            
        }
        public void ApplyForce(Vector3D force)
        { float X=(float)force.X; float Y=(float)force.Y; float Z=(float)force.Z;
            body.ApplyCentralForce(new Vector3(X,Y,Z));
        }
        
        public double CalculateMass(double volume)
        {
            return volume * density;
        }
       


        public void ApplyRigidBody()
        {
            force = mass * acceleration;
        }
        public Vector3D CalculateDistanceBetweenTwoPoints(Point3D a, Point3D b)
        {
            return new Vector3D(b.X-a.X, b.Y-a.Y, b.Z-a.Z);
        }
        public double CalculateTetrahedronVolume(Point3D a, Point3D b, Point3D c,Point3D d)
        {
            Vector3D AB = CalculateDistanceBetweenTwoPoints(a, b);
            Vector3D AC = CalculateDistanceBetweenTwoPoints(a, c);
            Vector3D AD = CalculateDistanceBetweenTwoPoints(a, d);
            double volume = (Vector3D.DotProduct(AB, (Vector3D.CrossProduct(AC, AD))));
            double volum=Math.Abs(1/6f *volume);
            return volum;
        }
        public double CalculateVolume(Point3DCollection vertices,Int32Collection indices)
        {
            double volume = 0.0;
            for(int i=0;i<indices.Count;i+=3)
            {
                Point3D a= vertices[indices[i]];
                Point3D b= vertices[indices[i+1]];
                Point3D c= vertices[indices[i+2]];
                Point3D origins = new Point3D(0, 0, 0);
                double volumeOfTetrahedron=CalculateTetrahedronVolume(a, b, c, origins);
                volume += volumeOfTetrahedron;
            }
            return volume;
        }
        
    }
}
