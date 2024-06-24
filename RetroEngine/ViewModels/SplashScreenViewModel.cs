using RetroEngine.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RetroEngine.ViewModels
{
    public class SplashScreenViewModel:BaseVM
    {
        private string _imagePath;
        double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                NotifyPropertyChanged("Progress");
            }
        }
        string _background;
        public string Background
        {
            get { return _background; }
            set
            {
                _background = value;
                NotifyPropertyChanged("Background");
            }
        }
     
        private string _titlePath;
        public string TitlePath
        {
            get { return _titlePath; }
            set
            {
                _titlePath = value;
                NotifyPropertyChanged("TitlePath");
            }
        } 
        

        public SplashScreenViewModel()
        {
            
            _titlePath = "../images/title.png";
            _background ="../images/load.gif";
        }
        public void SetProgress(double prog)
        {
            _progress = prog;
        }
      
    }
}
