using Assimp;
using FramworkFor3D._3DObjects;
using RetroEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            _clips= new List<AnimationClip>();
            foreach (var animation in scene.Animations)
            {
                AnimationClip clip = new AnimationClip();
                clip.Name = animation.Name;
                clip.duration = animation.DurationInTicks / (double)animation.TicksPerSecond;
                foreach (var channel in animation.NodeAnimationChannels)
                {
                    KeyFrame keyFrame = new KeyFrame();
                    keyFrame.NodeName = channel.NodeName;
                    foreach (var key in channel.PositionKeys)
                    {
                        keyFrame.Position.Add(new Vector3D(key.Value.X, key.Value.Y, key.Value.Z));
                    }
                    foreach(var key in channel.RotationKeys)
                    {
                        keyFrame.Rotation.Add(new Quaternion(key.Value.X, key.Value.Y, key.Value.Z, key.Value.W));
                    }
                    foreach(var key in channel.ScalingKeys)
                    {
                        keyFrame.Scale.Add(new Vector3D(key.Value.X, key.Value.Y, key.Value.Z));
                    }
                    clip.KeyFrames.Add(keyFrame);

            }
                _clips.Add(clip);

        }


    }
}
