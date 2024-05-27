﻿using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using FramworkFor3D._3DObjects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
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

        public RigidBody(float mass)
        {
            this.mass = mass;


           
        }
        public void Start(UIElement3D obj)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += (s, e) => Fall(s, e, obj);
            timer.Start();
        }

        

        public void Fall(object sender,EventArgs e, UIElement3D obj)
        {
          
         
             float fallingDirection =(float) obj.Transform.Value.OffsetZ;
            
           
            time_elapsed+=(float)timer.Interval.TotalSeconds;
           //formula pentru cadere libera a lui Newton dar fara viteza intiala si altitudine initiala obiectului
            fallingDirection -=(float)0.5*gravity_acceleration*time_elapsed*time_elapsed;
            TranslateTransform3D falling = new TranslateTransform3D();
            falling.OffsetZ= fallingDirection; 
            falling.OffsetX = obj.Transform.Value.OffsetX;
            falling.OffsetY = obj.Transform.Value.OffsetY;
           
            obj.Transform = falling;
            if(fallingDirection < 0.1f)
            {
                timer.Stop();
            }
          
        }
       





    }
}
