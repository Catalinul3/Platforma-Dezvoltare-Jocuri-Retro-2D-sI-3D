using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FramworkFor3D._3DSounds
{
    public class Audio
    {

        private Dictionary<string, MediaPlayer> _useSounds;
       
        public Audio()
        {    _useSounds=new Dictionary<string, MediaPlayer>();
            
        }
        public void LoadSound(string key,string fileName)

        {
            if(!_useSounds.ContainsKey(key))
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(fileName));
                _useSounds[key] = player;
            }
        }
        public void Play(string key)
        {
            if (_useSounds.ContainsKey(key))
            {
                MediaPlayer player = _useSounds[key];
                player.Play();
            }
        }
        public void AdjustVolume(int volume,string key)
        {
            if(_useSounds.ContainsKey(key))
            {
                MediaPlayer player = _useSounds[key];
                player.Volume = volume;
            }
        }
    }
}
