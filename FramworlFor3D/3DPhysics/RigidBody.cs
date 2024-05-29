using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using FramworkFor3D._3DObjects;
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
        float mass;
        const float gravity_acceleration = 9.81f;
        DispatcherTimer timer;
        UIElement3D obj;
        Rect3D bound;
        float time_elapsed = 0;
        bool colliding = false;
        double jumpVelocity=10.0f;
        double restitution = 0.6f;
        double jumpVelocityCopied = 10.0f;
        public Rect3D Bound
        {
            get { return bound; }
            set
            {
                this.bound = value;
            }
        }
     
        public RigidBody(float mass)
        {
            this.mass = mass;



        }
        public void Start(UIElement3D obj, Rect3D bound)
        {



            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Fall(s, e, obj, bound);
            timer.Start();



        }



        public void Fall(object sender, EventArgs e, UIElement3D obj, Rect3D bound)
        {


            float fallingDirection = (float)obj.Transform.Value.OffsetZ;


            time_elapsed += (float)timer.Interval.TotalSeconds;

            //formula pentru cadere libera a lui Newton dar fara viteza intiala si altitudine initiala obiectului
            fallingDirection -= (float)mass * 0.5f * gravity_acceleration * time_elapsed * time_elapsed;
            //Transform3DGroup transformationGroup = new Transform3DGroup();
            TranslateTransform3D falling = new TranslateTransform3D();
            falling.OffsetZ = fallingDirection;

            falling.OffsetX = obj.Transform.Value.OffsetX;
            falling.OffsetY = obj.Transform.Value.OffsetY;
            //transformationGroup.Children.Add(falling);
            obj.Transform = falling;

            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Bound = Collider.UpdateBounds(objectModel, falling);
            bool collision = Collider.IsColliding(Bound, bound);
            if (collision)
            {
                Stop();

            }



        }
        public void StartBouncing(UIElement3D obj)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Bounce(s, e, obj);
            timer.Start();
        }
        public void StartJump(UIElement3D obj)
        {
            double jump = jumpVelocity;
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Jump(s, e, obj,jumpVelocityCopied);
            timer.Start();

        }

        private void Jump(object s, EventArgs e, UIElement3D obj,double jumpVelocity)
        {
            float jumpingDirection = (float)obj.Transform.Value.OffsetZ;
            time_elapsed +=(float)timer.Interval.TotalSeconds;

            jumpingDirection += (float)jumpVelocity * 0.5f * gravity_acceleration * time_elapsed * time_elapsed;
            //Transform3DGroup transformationGroup = new Transform3DGroup();
            TranslateTransform3D jump = new TranslateTransform3D();
            jump.OffsetZ = jumpingDirection;
          
            jump.OffsetX = obj.Transform.Value.OffsetX;
            jump.OffsetY = obj.Transform.Value.OffsetY;
            //transformationGroup.Children.Add(falling);
            obj.Transform = jump;

            
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Bound = Collider.UpdateBounds(objectModel, jump);
            
            if (jumpVelocity<=0.0f)
            {
                Stop();
                time_elapsed = 0;
                StartBouncing(obj);
                
               
            }
            else
            {
                jumpVelocityCopied -= 1;
            }
          
         

        }

        private void Bounce(object s, EventArgs e, UIElement3D obj)
        {
            float fallingDirection = (float)obj.Transform.Value.OffsetZ;


            time_elapsed += (float)timer.Interval.TotalSeconds;

            //formula pentru cadere libera a lui Newton dar fara viteza intiala si altitudine initiala obiectului
            float jump = 0;
            fallingDirection -= (float)(mass * 0.5f * gravity_acceleration * time_elapsed * time_elapsed);
            //Transform3DGroup transformationGroup = new Transform3DGroup();

            //transformationGroup.Children.Add(falling);

            TranslateTransform3D falling = new TranslateTransform3D();
            falling.OffsetZ = fallingDirection;

            falling.OffsetX = obj.Transform.Value.OffsetX;
            falling.OffsetY = obj.Transform.Value.OffsetY;
            //transformationGroup.Children.Add(falling);
            obj.Transform = falling;
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Bound = Collider.UpdateBounds(objectModel, falling);

            if (obj.Transform.Value.OffsetZ <= 0.1f)
            {
              jumpVelocity*=restitution;
                jumpVelocityCopied = jumpVelocity;
               
                Stop();
                StartJump(obj);
                
            }
            if(jumpVelocity<=1f)
            {
                Stop();
            }
          
        

        }

        public void Stop()
        {
            timer.Stop();
        }
        public void ApplyForce(Vector3D forceAxis, float force)
        {

        }




    }
}
