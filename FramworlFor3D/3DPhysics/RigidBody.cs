using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using FramworkFor3D._3DObjects;
using FramworkFor3D.Commands;
using FramworkFor3D.helpers;
using HelixToolkit.Wpf;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shell;
using System.Windows.Threading;
using System.Xaml;
using Matrix = BulletSharp.Math.Matrix;

namespace FramworkFor3D._3DPhysics
{
    public class RigidBody
    {
        #region Variables
        float mass;
        const float gravity_acceleration = 9.81f;
        DispatcherTimer timer;
        UIElement3D obj;
        Rect3D bound;
        float time_elapsed = 0;
        bool colliding = false;
       float force = 0.0f;
        double jumpVelocity = 10.0f;
        double restitution = 1f;
        double jumpVelocityCopied = 10.0f;
        float acceleration = 0.0f;
        public float Mass
        {
            get { return mass; }
            set
            {
                this.mass = value;
            }
        }
        public Rect3D Bound
        {
            get { return bound; }
            set
            {
                this.bound = value;
            }
        }

        bool fromLeft = false;
        GameObjectCommands gameObj;
        #endregion

        #region Constructor
        public RigidBody(float mass,GameObjectCommands game)
        {
            this.mass = mass;
            gameObj = game;

        }
        #endregion

        #region Fall Physics
        public void Start(UIElement3D obj, ObjectType type)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Fall(s, e, obj, type);
            timer.Start();

        }

        public void Fall(object sender, EventArgs e, UIElement3D obj, ObjectType type)
        {


            float fallingDirection = (float)obj.Transform.Value.OffsetZ;


            time_elapsed += (float)timer.Interval.TotalSeconds;

            //formula pentru cadere libera a lui Newton dar fara viteza intiala si altitudine initiala obiectului
            fallingDirection -= (float)mass * 0.5f * gravity_acceleration * time_elapsed * time_elapsed;
            Transform3DGroup transformationGroup = new Transform3DGroup();
            TranslateTransform3D falling = new TranslateTransform3D();
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            if (type == ObjectType.IRREGULAR)
            {
                Irregular3DObject irregular = new Irregular3DObject(objectModel);

                if (irregular != null)
                {

                    irregular.Rotate(90, new Vector3D(1, 0, 0));
                    transformationGroup.Children.Add(irregular.Transform);
                    falling.OffsetZ = fallingDirection;

                    falling.OffsetX = obj.Transform.Value.OffsetX;
                    falling.OffsetY = obj.Transform.Value.OffsetY;
                    transformationGroup.Children.Add(falling);
                    obj.Transform = transformationGroup;

                }

            }
            else
            {
                falling.OffsetZ = fallingDirection;
                falling.OffsetX = obj.Transform.Value.OffsetX;
                falling.OffsetY = obj.Transform.Value.OffsetY;

                obj.Transform = falling;
            }




            Bound = Collider.UpdateBounds(objectModel, falling);

            bool collision = Collider.IsColliding(Bound, bound);
        

            if (fallingDirection <= 0.1f)
            {
                if (type == ObjectType.IRREGULAR)
                {
                    gameObj.ObjBounds = Bound;
                }
                if (type == ObjectType.SPHERE)
                {
                    gameObj.SphereBounds = Bound;
                }
                if (type == ObjectType.CUBE)
                {
                    gameObj.CubeBounds = Bound;
                }
                Stop();

            }


        }
        #endregion

        #region Bounce Physics
        public void StartBouncing(UIElement3D obj, float mass, ObjectType type)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Bounce(s, e, obj, mass, type);
            timer.Start();
        }
        public void StartJump(UIElement3D obj, ObjectType type)
        {
            double jump = jumpVelocity;
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Jump(s, e, obj, jumpVelocityCopied, type);
            timer.Start();

        }

        private void Jump(object s, EventArgs e, UIElement3D obj, double jumpVelocity, ObjectType type)
        {
            float jumpingDirection = (float)obj.Transform.Value.OffsetZ;
            time_elapsed += (float)timer.Interval.TotalSeconds;

            jumpingDirection += (float)jumpVelocity * 0.5f * gravity_acceleration * time_elapsed * time_elapsed;
            Transform3DGroup transformationGroup = new Transform3DGroup();
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);

            TranslateTransform3D jump = new TranslateTransform3D();
            if (type == ObjectType.IRREGULAR)
            {
                Irregular3DObject irregular = new Irregular3DObject(objectModel);

                if (irregular != null)
                {

                    irregular.Rotate(90, new Vector3D(1, 0, 0));
                    transformationGroup.Children.Add(irregular.Transform);
                    jump.OffsetZ = jumpingDirection;

                    jump.OffsetX = obj.Transform.Value.OffsetX;
                    jump.OffsetY = obj.Transform.Value.OffsetY;
                    transformationGroup.Children.Add(jump);
                    obj.Transform = transformationGroup;

                }

            }
            else
            {
                jump.OffsetZ = jumpingDirection;
                jump.OffsetX = obj.Transform.Value.OffsetX;
                jump.OffsetY = obj.Transform.Value.OffsetY;
                transformationGroup.Children.Add(jump);
                obj.Transform = transformationGroup;
            }

            Bound = Collider.UpdateBounds(objectModel, jump);

            if (jumpVelocity <= 0.0f)
            {
                Stop();
                time_elapsed = 0;
                StartBouncing(obj, mass, type);

            }
            else
            {
                jumpVelocityCopied -= 1;
            }

        }
        private void Bounce(object s, EventArgs e, UIElement3D obj, float mass, ObjectType type)
        {
            float fallingDirection = (float)obj.Transform.Value.OffsetZ;
            time_elapsed += (float)timer.Interval.TotalSeconds;

            //formula pentru cadere libera a lui Newton dar fara viteza intiala si altitudine initiala obiectului
            float jump = 0;
            fallingDirection -= (float)(mass * 0.5f * gravity_acceleration * time_elapsed * time_elapsed);
            Transform3DGroup transformationGroup = new Transform3DGroup();

            //transformationGroup.Children.Add(falling);
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);

            TranslateTransform3D falling = new TranslateTransform3D();
            if (type == ObjectType.IRREGULAR)
            {
                Irregular3DObject irregular = new Irregular3DObject(objectModel);

                if (irregular != null)
                {

                    irregular.Rotate(90, new Vector3D(1, 0, 0));
                    transformationGroup.Children.Add(irregular.Transform);
                    falling.OffsetZ = fallingDirection;

                    falling.OffsetX = obj.Transform.Value.OffsetX;
                    falling.OffsetY = obj.Transform.Value.OffsetY;
                    transformationGroup.Children.Add(falling);
                    obj.Transform = transformationGroup;

                }

            }
            else
            {
                falling.OffsetZ = fallingDirection;
                falling.OffsetX = obj.Transform.Value.OffsetX;
                falling.OffsetY = obj.Transform.Value.OffsetY;
                transformationGroup.Children.Add(falling);
                obj.Transform = transformationGroup;
            }
            Bound = Collider.UpdateBounds(objectModel, falling);

            if (obj.Transform.Value.OffsetZ <= 0.1f)
            {
                jumpVelocity *= restitution;
                jumpVelocityCopied = jumpVelocity;

                Stop();

                StartJump(obj, type);

            }
            if (jumpVelocity <= 1f)
            {
                Stop();
            }

        }
        #endregion

        #region Force Physics
        public void StartForce(Rect3D objWithForce, Rect3D objApplyForce, UIElement3D obj, UIElement3D obj2, ObjectType type, ObjectType affectByForce)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => AddForce(s, e, objWithForce, objApplyForce, obj, obj2, type, affectByForce);
            timer.Start();
        }
        public void StartForceWithUserCollider(Rect3D objWithForce, Rect3D objApplyForce, UIElement3D obj, UIElement3D obj2)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => AddForceUser(s, e, objWithForce, objApplyForce, obj, obj2);
            timer.Start();
        }
        public void AddForceUser(object s, EventArgs e, Rect3D objWithForce, Rect3D objApplyForce, UIElement3D obj, UIElement3D obj2)
        {
            float forceOrientation = (float)obj.Transform.Value.OffsetX;
            float distance = getDistance(Bound, objApplyForce);
            time_elapsed += (float)timer.Interval.TotalSeconds;
            ObjectType type = ObjectType.SPHERE;
            forceOrientation += (float)(mass * time_elapsed * time_elapsed);
            Transform3DGroup transformationGroup = new Transform3DGroup();
            TranslateTransform3D move = new TranslateTransform3D();
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Irregular3DObject irregular = new Irregular3DObject(objectModel);
            if (irregular != null)
            {
                irregular.Rotate(90, new Vector3D(1, 0, 0));
                transformationGroup.Children.Add(irregular.Transform);
            }
            move.OffsetX = forceOrientation;
            move.OffsetY = obj.Transform.Value.OffsetY;
            move.OffsetZ = obj.Transform.Value.OffsetZ;
            transformationGroup.Children.Add(move);
            obj.Transform = transformationGroup;

            Bound = Collider.UpdateBounds(objectModel, move);
            objWithForce = Collider.UpdateColliderObject(objWithForce, move);
            Bound = objWithForce;
            if (Collider.IsColliding(Bound, objApplyForce))
            {
                float forcePower = (float)obj2.Transform.Value.OffsetX;
                float velocity = (float)(distance / time_elapsed);
                float acceleration = (float)(velocity / time_elapsed);
                float force = (float)(mass * acceleration);
                forcePower += force / 100;
                Stop();
                StartFriction(obj2,  velocity, type);

            }

        }
        public void AddForce(object s, EventArgs e, Rect3D objWithForce, Rect3D objApplyForce,
            UIElement3D obj, UIElement3D obj2, ObjectType typeObjectForce, ObjectType typeObjectAffected)
        {
            float forceOrientation = (float)obj.Transform.Value.OffsetX;
            float distance = getDistance(objWithForce, objApplyForce);

            time_elapsed += (float)timer.Interval.TotalSeconds;
            if (objWithForce.X >= objApplyForce.X)
            {
                forceOrientation -= (float)(mass * time_elapsed * time_elapsed);

            }
            else
            {
                forceOrientation += (float)(mass * time_elapsed * time_elapsed);
                fromLeft = true;
            }
            TranslateTransform3D move = new TranslateTransform3D();
            Transform3DGroup transformationGroup = new Transform3DGroup();

            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Irregular3DObject irregular = new Irregular3DObject(objectModel);
            if (typeObjectForce == ObjectType.IRREGULAR)
            {

                if (irregular != null)
                {
                    irregular.Rotate(90, new Vector3D(1, 0, 0));
                    transformationGroup.Children.Add(irregular.Transform);
                }
                move.OffsetX = forceOrientation;
                move.OffsetY = obj.Transform.Value.OffsetY;
                move.OffsetZ = obj.Transform.Value.OffsetZ;
                transformationGroup.Children.Add(move);
                obj.Transform = transformationGroup;
            }
            else
            {
                move.OffsetX = forceOrientation;
                move.OffsetZ = obj.Transform.Value.OffsetZ;
                move.OffsetY = obj.Transform.Value.OffsetY;

                obj.Transform = move;
            }

            Bound = Collider.UpdateBounds(objectModel, move);

            if (Collider.IsColliding(Bound, objApplyForce))
            {
                float forcePower = (float)obj2.Transform.Value.OffsetX;
                float velocity = (float)(distance / time_elapsed);
                float acceleration = (float)(velocity / time_elapsed);
                force = (float)((mass * acceleration)/200);
              
                Stop();

                StartFriction(obj2, velocity, typeObjectAffected);

            }
        }
        public void StartFriction(UIElement3D obj,  float velocity, ObjectType type)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => AddFriction(s, e, obj, velocity, type);
            timer.Start();
        }
        public void AddFriction(object s, EventArgs e, UIElement3D obj, float velocity, ObjectType type)
        {

            float forceDirection = (float)obj.Transform.Value.OffsetX;
            time_elapsed += (float)timer.Interval.TotalSeconds;
            if (fromLeft)
            {
                forceDirection += (float)(force * time_elapsed * time_elapsed);
                force -= forceDirection;
            }
            else
            {
                forceDirection -= (float)(force * time_elapsed * time_elapsed);
                force -= force;
            }

            TranslateTransform3D move = new TranslateTransform3D();
            Transform3DGroup transformationGroup = new Transform3DGroup();
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            if (type == ObjectType.IRREGULAR)
            {
                Irregular3DObject irregular = new Irregular3DObject(objectModel);
                if (irregular != null)
                {
                    irregular.Rotate(90, new Vector3D(1, 0, 0));
                    transformationGroup.Children.Add(irregular.Transform);
                }
                move.OffsetX = forceDirection;

                move.OffsetY = obj.Transform.Value.OffsetY;
                move.OffsetZ = obj.Transform.Value.OffsetZ;
                transformationGroup.Children.Add(move);
                obj.Transform = transformationGroup;
            }
            else
            {

                move.OffsetX = forceDirection;



                move.OffsetZ = obj.Transform.Value.OffsetZ;
                move.OffsetY = obj.Transform.Value.OffsetY;

                obj.Transform = move;
            };

            Bound = Collider.UpdateBounds(objectModel, move);
            if (force < 0)
            { Stop(); }

        }
        #endregion

        #region Helpers
        private float getDistance(Rect3D obj1, Rect3D obj2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((obj2.X - obj1.X), 2)
                + Math.Pow((obj2.Y - obj1.Y), 2) +
                Math.Pow((obj2.Z - obj1.Z), 2)));
            //MessageBox.Show("Distance: " + distance);
            return distance;
        }
        public void Stop()
        {
            timer.Stop();
        }
        public Rect3D getBound()
        {
            return Bound;
        }
        #endregion
    }
}
