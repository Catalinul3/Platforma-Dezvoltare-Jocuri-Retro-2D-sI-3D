using Assimp;
using FramworkFor3D._3DObjects;
using FramworkFor3D.helpers;
using HelixToolkit.Wpf;

using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.Animations
{
    public class Animation
    {
        List<Irregular3DObject> _irregular3DObjects;
        Scene scene;
        List<AnimationClip> _clips;
        public List<Irregular3DObject> Irregular3DObjects
        {
            get { return _irregular3DObjects; }
            set { _irregular3DObjects = value; }
        }

        public Animation()
        {
            string file = FileHelpers.loadFBXDialog("Load FBX File");
            scene = FBXHelper.readFile(file);
            _clips = new List<AnimationClip>();
            foreach (var animation in scene.Animations)
            {
                AnimationClip clip = new AnimationClip
                {
                    Name = animation.Name,
                    Frames = new List<AnimationFrame>()
                };
                foreach (var frames in animation.NodeAnimationChannels)
                {
                    AnimationFrame frame = new AnimationFrame
                    {
                        Time = frames.PositionKeys[0].Time,
                        transformation = new Dictionary<string, Transform3D>()
                    };
                    foreach (var key in frames.PositionKeys)
                    {
                        Transform3D transform = new Transform3DGroup();
                        System.Windows.Media.Media3D.Vector3D value = convertToVector3D(key.Value);
                        ((Transform3DGroup)transform).Children.Add(new TranslateTransform3D(value));
                        frame.transformation.Add(key.Time.ToString(), transform);
                    }
                    clip.Frames.Add(frame);

                }
                _clips.Add(clip);
            }
        }
        public void StartAnimation(ModelVisual3D model)
        {
            if (_clips.Count > 0)
            {
                MessageBox.Show("Name of animation is " + _clips[2].Name);
                ApplyAnimation(_clips[2], model);
            }
        }
        private void ApplyAnimation(AnimationClip clip, ModelVisual3D model)
        {
           
            Storyboard storyBoard = new Storyboard();
            storyBoard.RepeatBehavior = RepeatBehavior.Forever;
            foreach (var frame in clip.Frames)
            {
                foreach (var transformation in frame.transformation)
                {
                    var transform = transformation.Value.Value;
                    if (transform != null)
                    {
                        Matrix3D matrix = transform;
                        System.Windows.Media.Media3D.Vector3D translate = new System.Windows.Media.Media3D.Vector3D(matrix.OffsetX, matrix.OffsetY, matrix.OffsetZ);

                        var translateTransform = new TranslateTransform3D(translate);
                 
                       
                        if (translateTransform != null)
                        {
                            // Crearea animației pentru OffsetX
                            DoubleAnimation translateXAnimation = new DoubleAnimation();
                            translateXAnimation.From = translateTransform.OffsetX;
                            translateXAnimation.To = translateTransform.OffsetX+10;
                            translateXAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(frame.Time));

                            // Setarea țintei și a proprietății pentru animație
                            Storyboard.SetTarget(translateXAnimation, translateTransform);
                            Storyboard.SetTargetProperty(translateXAnimation, new PropertyPath(TranslateTransform3D.OffsetXProperty));

                            // Adăugarea animației la storyboard
                            storyBoard.Children.Add(translateXAnimation);
                            DoubleAnimation translateYAnimation = new DoubleAnimation
                            {
                                From = translateTransform.OffsetY,
                                To = translateTransform.OffsetY+1,
                                Duration = new Duration(TimeSpan.FromMilliseconds(frame.Time))
                            };
                            Storyboard.SetTarget(translateYAnimation, translateTransform);
                            Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath(TranslateTransform3D.OffsetYProperty));
                            storyBoard.Children.Add(translateYAnimation);
                            DoubleAnimation translateZAnimation = new DoubleAnimation
                            {
                                From = translateTransform.OffsetZ,
                                To = translateTransform.OffsetZ + 1,
                                Duration = new Duration(TimeSpan.FromMilliseconds(frame.Time))
                            };
                            Storyboard.SetTarget(translateZAnimation, translateTransform);
                            Storyboard.SetTargetProperty(translateZAnimation, new PropertyPath(TranslateTransform3D.OffsetZProperty));
                            storyBoard.Children.Add(translateZAnimation);
                        }
                    }
                }
            }
            storyBoard.Begin();
        }


        protected System.Windows.Media.Media3D.Vector3D convertToVector3D(Assimp.Vector3D value)
        {
            return new System.Windows.Media.Media3D.Vector3D(value.X, value.Y, value.Z);
        }
    }
}
