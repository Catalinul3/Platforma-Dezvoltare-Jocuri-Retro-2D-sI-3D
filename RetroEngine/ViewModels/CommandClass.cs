using RetroEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RetroEngine.ViewModels
{
    public class CommandClass:BaseVM
    {
        private RelayCommand _createGame;
        private RelayCommand _exitGame;
       
        public RelayCommand createGame
        {
            get
            {
                if(_createGame == null)
                    _createGame = new RelayCommand(Create);
                return _createGame;
            }
        }
        public RelayCommand exitGame
        {
            get
            {
                if(_exitGame==null)
                    
                    _exitGame = new RelayCommand(Exit);
                return _exitGame;
            }
        }

        private void Exit(object parameter)
        {
            MessageBox.Show("You gave up");
        }

        private void Create(object parameter)
        {
           if(parameter is Image image)
            { string path = "../images/create_image.png";
                BitmapImage source = new BitmapImage(new Uri(path,UriKind.Relative));
                image.Source = source;
                
                
            }
        }
    }
}
