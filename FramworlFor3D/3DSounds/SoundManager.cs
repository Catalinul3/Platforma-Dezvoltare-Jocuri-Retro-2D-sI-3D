using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D._3DSounds
{
    public class SoundManager
    {
        List<Audio> audioClips;
        List<BackgroundMusic> backClip;
        public SoundManager()
        {
            audioClips = new List<Audio>();
            backClip = new List<BackgroundMusic>();
        }
        public void AddMusic(BackgroundMusic music)
        {
            backClip.Add(music);
        }
        public void AddSound(Audio clip)
        {
            audioClips.Add(clip);
        }
        public void RemoveSound(Audio clip)
        {
            audioClips.Remove(clip);
        }
        public void RemoveSound(BackgroundMusic music)
        {
            backClip.Remove(music);
        }
        public void playBackground(string tag)
        {
            foreach (var clip in backClip)
            {
                if (clip.getKey(tag))
                {
                    clip.play(tag); break;
                }
            }
        }
        public void PlaySound(string tag)
        {

            foreach (var clip in audioClips)
            {
                if (clip.getKey(tag))
                {
                    clip.Play(tag); break;
                }
            }
            ;
        }
        public void stop(string tag)
        {

            foreach (var clip in audioClips)
            {
                if (clip.getKey(tag))
                {
                    clip.Stop(tag);
                }
            }
        }
        public void looping(string tag)
        {
            foreach (var clip in audioClips)
            {
                if (clip.getKey(tag))
                {
                    clip.Looping(tag);
                }
            }
        }

    }
}
