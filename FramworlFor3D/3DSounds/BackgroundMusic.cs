using RetroEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FramworkFor3D._3DSounds
{
    public class BackgroundMusic
    {
        private Dictionary<string, MediaPlayer> music;
        float volume;
        public Dictionary<string, MediaPlayer> Music
        {
            get
            {
                return music;
            }
            set { music = value; }
        }
        public BackgroundMusic()
        {
            music = new Dictionary<string, MediaPlayer>();
            volume = 0.5f;
        }
        public void LoadMusic(string tag,string name)
        {
          
            MediaPlayer value = new MediaPlayer();
            value.Open(new Uri(name));
            value.MediaOpened += (s, e) =>
            {
                Console.WriteLine("Music completely succes");
               
            };
            music.Add(tag, value);
         
        }
        public void play(string name)
        {
            
                MediaPlayer player = Music[name];
                player.Play();
            
        }
        public void stop()
        {
            foreach (var player in music)
            {
                player.Value.Stop();
            }
     
        }
        public void Pause(string name)
        {
            if (music.ContainsKey(name))
            {
                MediaPlayer player = music[name];
                player.Pause();
            }
        }
        public void Resume(string name)
        {
            if(music.ContainsKey(name))
            {
                MediaPlayer player = music[name];
                player.Play();
            }
        }
        public void adjustVolume(int volume,string name)
        {
            MediaPlayer player = music[name];
            player.Volume += volume;
        }
        public MediaPlayer getMusicByTag(string tag)
        {
            return music[tag];
        }

        public bool getKey(string tag)
        {
           foreach(var key in music.Keys)
            {
                if(key==tag) return true;
            }
            return false;
        }
    }
}
