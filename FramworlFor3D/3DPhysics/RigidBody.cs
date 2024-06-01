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
     
        public RigidBody(float mass)
        {
            this.mass = mass;



        }
        public void Start(UIElement3D obj)
        {



            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Fall(s, e, obj);
            timer.Start();



        }



        public void Fall(object sender, EventArgs e, UIElement3D obj)
        {


            float fallingDirection = (float)obj.Transform.Value.OffsetZ;


            time_elapsed += (float)timer.Interval.TotalSeconds;

            //formula pentru cadere libera a lui Newton dar fara viteza intiala si altitudine initiala obiectului
            fallingDirection -= (float)mass * 0.5f * gravity_acceleration * time_elapsed * time_elapsed;
            Transform3DGroup transformationGroup = new Transform3DGroup();
            TranslateTransform3D falling = new TranslateTransform3D();
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Irregular3DObject irregular = new Irregular3DObject(objectModel) ;
            if(irregular!=null)
            {
                irregular.Rotate(90, new Vector3D(1, 0, 0));
                transformationGroup.Children.Add(irregular.Transform);
            }
      

                falling.OffsetZ = fallingDirection;

                falling.OffsetX = obj.Transform.Value.OffsetX;
                falling.OffsetY = obj.Transform.Value.OffsetY;
                transformationGroup.Children.Add(falling);
                obj.Transform = transformationGroup;
            
          

         
            Bound = Collider.UpdateBounds(objectModel, falling);
            bool collision = Collider.IsColliding(Bound, bound);
            if (fallingDirection<=0.1f)
            {
                Stop();

            }



        }
        public void StartBouncing(UIElement3D obj,float mass)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => Bounce(s, e, obj,mass);
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
            Transform3DGroup transformationGroup = new Transform3DGroup();
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Irregular3DObject irregular = new Irregular3DObject(objectModel);
            if (irregular != null)
            {
                irregular.Rotate(90, new Vector3D(1, 0, 0));
                transformationGroup.Children.Add(irregular.Transform);
            }
            TranslateTransform3D jump = new TranslateTransform3D();
            jump.OffsetZ = jumpingDirection;
          
            jump.OffsetX = obj.Transform.Value.OffsetX;
            jump.OffsetY = obj.Transform.Value.OffsetY;
            transformationGroup.Children.Add(jump);
            obj.Transform = transformationGroup;

            
           
            Bound = Collider.UpdateBounds(objectModel, jump);
            
            if (jumpVelocity<=0.0f)
            {
                Stop();
                time_elapsed = 0;
                StartBouncing(obj,mass);
                
               
            }
            else
            {
                jumpVelocityCopied -= 1;
            }
          
         

        }

        private void Bounce(object s, EventArgs e, UIElement3D obj,float mass)
        {
            float fallingDirection = (float)obj.Transform.Value.OffsetZ;


            time_elapsed += (float)timer.Interval.TotalSeconds;

            //formula pentru cadere libera a lui Newton dar fara viteza intiala si altitudine initiala obiectului
            float jump = 0;
            fallingDirection -= (float)(mass * 0.5f * gravity_acceleration * time_elapsed * time_elapsed);
            Transform3DGroup transformationGroup = new Transform3DGroup();

            //transformationGroup.Children.Add(falling);
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Irregular3DObject irregular = new Irregular3DObject(objectModel);
            if (irregular != null)
            {
                irregular.Rotate(90, new Vector3D(1, 0, 0));
                transformationGroup.Children.Add(irregular.Transform);
            }
            TranslateTransform3D falling = new TranslateTransform3D();
            falling.OffsetZ = fallingDirection;

            falling.OffsetX = obj.Transform.Value.OffsetX;
            falling.OffsetY = obj.Transform.Value.OffsetY;
            transformationGroup.Children.Add(falling);
            obj.Transform = transformationGroup;
       
            Bound = Collider.UpdateBounds(objectModel, falling);
       
         
            if (obj.Transform.Value.OffsetZ <= 0.3f)
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
        public void StartForce(Rect3D objWithForce,Rect3D objApplyForce,UIElement3D obj,UIElement3D obj2)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => AddForce( s, e,objWithForce,objApplyForce,obj,obj2);
            timer.Start();
        }
        public void AddForce(object s, EventArgs e, Rect3D objWithForce, Rect3D objApplyForce,UIElement3D obj,UIElement3D obj2)
        {   float forceOrientation=(float)obj.Transform.Value.OffsetX;
            float distance = getDistance(Bound, objApplyForce);
            time_elapsed+=(float)timer.Interval.TotalSeconds;
        
            forceOrientation += (float)(mass * time_elapsed * time_elapsed);
            TranslateTransform3D move= new TranslateTransform3D();
              move.OffsetX= forceOrientation;
                move.OffsetY=obj.Transform.Value.OffsetY;
            move.OffsetZ=obj.Transform.Value.OffsetZ;
             obj.Transform=move;
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Bound = Collider.UpdateBounds(objectModel, move);
             if(Collider.IsColliding(Bound,objApplyForce))
            {
                float forcePower = (float)obj2.Transform.Value.OffsetX;
                float velocity = (float)(distance / time_elapsed);
                float acceleration = (float)(velocity / time_elapsed);
                float force = (float)(mass * acceleration);
                forcePower += force/100;
                Stop();
                StartFriction(obj2,forcePower,velocity);
              

            }

          
        }
        public void StartFriction(UIElement3D obj,float force,float velocity)
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += (s, e) => AddFriction(s, e, obj,force,velocity);
            timer.Start();
        }
        public void AddFriction(object s, EventArgs e, UIElement3D obj,float force,float velocity)
        {
           
           float forcePower=(float)obj.Transform.Value.OffsetX;
            time_elapsed+=(float)timer.Interval.TotalSeconds;
            forcePower += (float)(velocity * time_elapsed*time_elapsed);
            force-=forcePower;
            TranslateTransform3D move = new TranslateTransform3D();
            move.OffsetX = forcePower;
            move.OffsetY = obj.Transform.Value.OffsetY;
            move.OffsetZ = obj.Transform.Value.OffsetZ;
            obj.Transform = move;
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            Bound = Collider.UpdateBounds(objectModel, move);
            if(force<0)
            { Stop(); }

        }
        private float getDistance(Rect3D obj1,Rect3D obj2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((obj2.SizeX - obj2.X) - (obj1.SizeX - obj1.X), 2)
                + Math.Pow((obj2.SizeY - obj2.Y) - (obj1.SizeY - obj1.Y), 2) +
                Math.Pow((obj2.SizeZ - obj2.Z) - (obj1.SizeZ - obj2.Z), 2)));
            //MessageBox.Show("Distance: " + distance);
            return distance;
        }




    }
}
