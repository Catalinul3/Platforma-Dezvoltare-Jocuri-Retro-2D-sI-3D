using RetroEngine.Helpers;
using RetroEngine.View;
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
            System.Windows.Application.Current.Shutdown();
        }

        private void Create(object parameter)
        {

            CreateDialog dialog = new CreateDialog();
            dialog.ShowDialog();

                
            }
        }
    }

