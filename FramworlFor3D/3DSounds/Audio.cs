
using System.Collections.Generic;

using System.Media;


namespace FramworkFor3D._3DSounds
{
    public class Audio
    {

        private Dictionary<string, SoundPlayer> _useSounds;
        public Dictionary<string, SoundPlayer> useSounds
        {
            get
            {
                return _useSounds;
            }
            set
            {
                this._useSounds = value;
            }
        }
       
        public Audio()
        {    _useSounds=new Dictionary<string, SoundPlayer>();
            
        }
    
        public void LoadSound(string key,string fileName)

        {
            if(!_useSounds.ContainsKey(key))
            {
               SoundPlayer player = new SoundPlayer(fileName);
                player.Load();
                _useSounds[key] = player;
            }
        }
        public void Play(string key)
        {
            if (_useSounds.ContainsKey(key))
            {
                SoundPlayer player = _useSounds[key];
                player.Play();
            }
        }
        
        public void Stop(string key)
        {
            if (_useSounds.ContainsKey(key))
            {
                SoundPlayer player = _useSounds[key];
                player.Stop();
            }
        }   
        
    
        public void Looping(string key)
        {
            if (_useSounds.ContainsKey(key))
            {
                SoundPlayer player = _useSounds[key];
                player.PlayLooping();
            }
        }
  
        public bool getKey(string tags)
        {
           foreach(string data in _useSounds.Keys)
            {
if (data == tags)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
