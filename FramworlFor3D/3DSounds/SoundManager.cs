using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D._3DSounds
{
   public class SoundManager
    {
        List<Audio> audioClips;
                public SoundManager()
        {
            audioClips = new List<Audio>();
        }
        public void LoadSound(string key, string fileName)
        {
            Audio audio = new Audio();
            audio.LoadSound(key, fileName);
            audioClips.Add(audio);
        }
        public void Play(string key)
        {
            foreach (Audio audio in audioClips)
            {
                audio.Play(key);
            }
        }
        public void AdjustVolume(int volume, string key)
        {
            foreach (Audio audio in audioClips)
            {
                audio.AdjustVolume(volume, key);
            }
        }
    }
}
